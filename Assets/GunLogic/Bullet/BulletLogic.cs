using System.Collections;
using UnityEngine;


/// <summary>
/// Логика пули
/// </summary>
public class BulletLogic : MonoBehaviour
{
    [SerializeField] GameObject destroyEffect;
    public void Start()
    {
        StartCoroutine(DelayBulletDestroyCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DelayBulletDestroyCoroutine()
    {
        yield return new WaitForSeconds(14);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (destroyEffect != null)
        Instantiate(destroyEffect, transform.position, transform.rotation);
    }
}
