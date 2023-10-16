using UnityEngine;

public class ZValueDetection : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float maxHeight = 2f;

    private void Update()
    {
        UpdateZPosition();
    }

    private void UpdateZPosition()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, transform.forward, out hit, maxHeight, collisionLayer))
        {
            Vector3 newPos = transform.position;
            newPos.z = hit.point.z - 0.2f;
            transform.position = newPos;
        }

        Debug.DrawRay(raycastOrigin.position, transform.forward * maxHeight, Color.red);
    }
}