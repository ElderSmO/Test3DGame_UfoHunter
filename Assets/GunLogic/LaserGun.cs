using Assets.GunLogic;
using System.Collections;
using UnityEngine;

public class LaserGun : MonoBehaviour, IGun
{
    [SerializeField] AudioClip shootSound;
    [SerializeField] GameObject shootEffect;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPref;
    [SerializeField] int gun_id = 1;
    [SerializeField] float delayShootTime = 0.4f;
    [SerializeField] float bulletSpeed = 10f;
    Animator animator;
    bool canShoot;
    AudioSource audioSource;

    public int Gun_id { get => gun_id; set => gun_id = value; }

    public AudioClip ShootSound { get => shootSound; set => shootSound = value; }

    
    
    /// <summary>
    /// Стрельба
    /// </summary>
    public void Shoot()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPref, shootPoint.position, shootPoint.rotation);
            if (audioSource!=null) audioSource.Play();
            Instantiate(shootEffect, shootPoint.position, shootPoint.rotation);
            animator.SetTrigger("ShootTrigger");
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Camera playerCamera = Camera.main;

            // Создаем луч из центра экрана
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (rb != null)
            {
                rb.velocity = ray.direction * bulletSpeed;
            }
            animator.SetTrigger("NoShootTrigger");
            StartCoroutine(DelayShootCoroutine());
            canShoot = false;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && shootSound != null) audioSource.clip = shootSound;
        animator = GetComponent<Animator>();
        PlayerEvents.PlayerShoting += Shoot;
        canShoot=true;
    }

   

    IEnumerator DelayShootCoroutine()
    {
        yield return new WaitForSeconds(delayShootTime);
        canShoot = true;
    }
    
}
