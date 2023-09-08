using UnityEngine;

public class GunFollowMouseUI : MonoBehaviour
{
    [SerializeField] private float minYRotation = -30f;
    [SerializeField] private float maxYRotation = 30f;
    [SerializeField] private float rotationSpeed = 5f;

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

        Vector3 mouseWorldPos = cam.ScreenToViewportPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));//10 is the distance of the camera to the gun

        float newRotationX = Mathf.Clamp(mouseWorldPos.y - gunTransform.position.y, minYRotation, maxYRotation);

        newRotationX *= rotationSpeed;

        gunTransform.localEulerAngles = new Vector3(newRotationX, gunTransform.localEulerAngles.y, gunTransform.localEulerAngles.z);
    }
}