using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Header("Drag Configuration")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float returnSpeedMultiplier;

    private Vector3 initialPosition;
    private bool isDragging = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 movement = new Vector3(-mouseX, -mouseY, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, initialPosition);
            float returnSpeed = moveSpeed * returnSpeedMultiplier / distance;
            transform.position = Vector3.Lerp(transform.position, initialPosition, Mathf.Clamp01(returnSpeed * Time.deltaTime));
        }
    }
}