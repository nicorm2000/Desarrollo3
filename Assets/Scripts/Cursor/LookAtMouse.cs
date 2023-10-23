using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Camera mainCamera;

    public PlayerData playerData;

    private void Start()
    {
        mainCamera = Camera.main;
        playerData.transform = transform.parent;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseScreenPosition = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 offsetMousePosition = new Vector3(mousePosition.x, mousePosition.y, mouseScreenPosition.z);

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(offsetMousePosition);
        Vector3 directionToMouse = mouseWorldPosition - transform.position;
        float rotationAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        if (rotationAngle > 90 || rotationAngle < -90)
        {
            transform.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        playerData.transform.position = transform.position;
    }
}