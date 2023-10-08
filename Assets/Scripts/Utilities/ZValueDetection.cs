using UnityEngine;

public class ZValueDetection : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask collisionLayer;

    private void Update()
    {
        UpdateZPosition();
    }

    private void UpdateZPosition()
    {
        RaycastHit hit;

        Debug.DrawRay(raycastOrigin.position, transform.forward, Color.red);

        if (Physics.Raycast(raycastOrigin.position, transform.forward, out hit, 1f, collisionLayer))
        {
            Vector3 newPos = transform.position;
            newPos.z = hit.point.z - 0.2f;
            transform.position = newPos;
        }
    }
}