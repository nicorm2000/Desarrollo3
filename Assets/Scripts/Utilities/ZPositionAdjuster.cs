using UnityEngine;

public class ZPositionAdjuster : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private float zOffset = 0.5f;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            float newZ = hit.point.z + zOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }

        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
    }
}