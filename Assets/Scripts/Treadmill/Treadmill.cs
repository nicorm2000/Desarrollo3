using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField] private float treadmillSpeed = 5f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * treadmillSpeed;
        }
    }
}