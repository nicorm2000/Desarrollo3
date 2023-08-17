using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform.parent;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        Vector3 directionToMouse = mouseWorldPosition - transform.position;
        float rotationAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        //C
        //A
        //M
        //B
        //I
        //A
        //R
        //E
        //S
        //C
        //A
        //L
        //A
        // Flip the object horizontally if it crosses the 90-degree threshold
        if (rotationAngle > 90 || rotationAngle < -90)
        {
            transform.localScale = new Vector3(0.1f, -0.1f, 0.1f); // Flip horizontally
        }
        else
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Reset scale
        }

        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        playerTransform.position = transform.position;
    }
}