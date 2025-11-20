
using UnityEngine;
using UnityEngine.Events;

public class ObjectGameButtonControl : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonClick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            OnButtonHit();
        }
    }

    private void OnButtonHit()
    {
        onButtonClick?.Invoke();
    }
}
