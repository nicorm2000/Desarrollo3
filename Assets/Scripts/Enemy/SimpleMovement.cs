using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Get the input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        // Move the object based on the input
        transform.Translate(movement * speed * Time.deltaTime);
    }
}