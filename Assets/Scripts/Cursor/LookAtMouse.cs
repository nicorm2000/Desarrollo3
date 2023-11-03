using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// Sets the transform parent of the player data.
    /// </summary>
    private void Start()
    {
        SetPlayerDataTransformParent();
    }

    /// <summary>
    /// Updates the scale and rotation of the object based on the mouse position.
    /// </summary>
    private void Update()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        float rotationAngle = CalculateRotationAngle(mouseWorldPosition);
        SetScaleAndRotation(rotationAngle);
    }

    /// <summary>
    /// Retrieves the mouse position in the world space.
    /// </summary>
    /// <returns>The mouse position in the world space.</returns>
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offsetMousePosition = new Vector3(mousePosition.x, mousePosition.y, mouseScreenPosition.z);

        return Camera.main.ScreenToWorldPoint(offsetMousePosition);
    }

    /// <summary>
    /// Calculates the rotation angle based on the mouse position.
    /// </summary>
    /// <param name="mouseWorldPosition">The mouse position in the world space.</param>
    /// <returns>The rotation angle.</returns>
    private float CalculateRotationAngle(Vector3 mouseWorldPosition)
    {
        Vector3 directionToMouse = mouseWorldPosition - transform.position;
        return Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Sets the scale and rotation of the object based on the rotation angle.
    /// </summary>
    /// <param name="rotationAngle">The rotation angle.</param>
    private void SetScaleAndRotation(float rotationAngle)
    {
        transform.localScale = new Vector3(1f, rotationAngle > 90 || rotationAngle < -90 ? -1f : 1f, 1f);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    /// <summary>
    /// Sets the transform parent of the player data.
    /// </summary>
    private void SetPlayerDataTransformParent()
    {
        playerData.transform = transform.parent;
    }
}