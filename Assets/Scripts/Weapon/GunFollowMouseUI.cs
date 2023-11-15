using UnityEngine;

public class GunFollowMouseUI : MonoBehaviour
{
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;
    [SerializeField] private float rotationSpeed;

    private Transform gunTransform;
    private Camera cam;

    private void Start()
    {
        gunTransform = transform;
        cam = Camera.main;
    }
    private void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = cam.ScreenToViewportPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

        float newRotationX = Mathf.Clamp(mouseWorldPos.y - gunTransform.position.y, minYRotation, maxYRotation);

        newRotationX *= rotationSpeed;

        gunTransform.localEulerAngles = new Vector3(newRotationX, gunTransform.localEulerAngles.y, gunTransform.localEulerAngles.z);
    }
}