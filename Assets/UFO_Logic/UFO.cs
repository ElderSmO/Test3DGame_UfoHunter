using Assets.GameControl;
using Assets.UFO_Logic;
using System.Collections;
using UnityEngine;


/// <summary>
/// НЛО
/// </summary>
public class UFO : MonoBehaviour, IEnemy
{
    [SerializeField] float speed;
    [SerializeField] string nameEnemy;
    [SerializeField] int hp;
    [SerializeField] float changeVectorTime;
    [SerializeField] private bool startMovingRight = true;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] GameObject LoseText;
    Rigidbody rb;

    
    private Vector3 currentDirection;
    private bool movingRight;

    public float ChangeVectorTime { get => changeVectorTime; set => changeVectorTime = value; }

    public bool StartMovingRight { get => startMovingRight; set => startMovingRight = value; }

    public float Speed { get => speed; set => speed = value; }
    public string NameEnemy { get => nameEnemy; set => nameEnemy = value; }
    public int Hp { get => hp; set => hp = value; }

    /// <summary>
    /// Передвижение
    /// </summary>
    public void Move()
    {
        rb.velocity = currentDirection * speed;
    }

    /// <summary>
    /// Смена траектории
    /// </summary>
    private void ChangeDirection()
    {
        
        if (movingRight)
        {
            currentDirection = Vector3.right; 
        }
        else
        {
            currentDirection = Vector3.left; 
        }
        
        movingRight = !movingRight;

        
        // currentDirection = new Vector3(currentDirection.x, -0.2f, 0f);
    }

    /// <summary>
    /// Получение урона 
    /// </summary>
    public void TakeDamage()
    {
        if (hp >=0)
        {
            hp -= 1;
            if (hp <= 0)
            {
                if (destroyEffect != null)
                    Instantiate(destroyEffect, transform.position, transform.rotation);
                    UfoGameEvents.OnEnemyDead();
                    Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movingRight = startMovingRight; 
        ChangeDirection();
        StartCoroutine(ChangeMoveVectorCoroutine());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DeadZone") // Логика мертвой зоны
        {
            if (LoseText != null)
                Instantiate(LoseText, transform.position, transform.rotation);
            UfoGameEvents.OnEnemyInDeadZone(gameObject);
            UfoGameEvents.OnEnemyLose();
            Destroy(gameObject);

        }
    }

    void Update()
    {
        Move();
    }
    IEnumerator ChangeMoveVectorCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeVectorTime);
            ChangeDirection();
        }
    }
}