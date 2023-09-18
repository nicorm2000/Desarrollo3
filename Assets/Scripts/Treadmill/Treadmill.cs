using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField] private float treadmillSpeed = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.right * treadmillSpeed;
        }
    }
}