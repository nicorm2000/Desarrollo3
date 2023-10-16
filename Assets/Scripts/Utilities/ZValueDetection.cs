using UnityEngine;

public class ZValueDetection : MonoBehaviour
{
    [SerializeField] private Transform raycastTop;
    [SerializeField] private Transform raycastBot;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float maxHeight = 2f;

    private void Update()
    {
        UpdateZPosition();
    }

    private void UpdateZPosition()
    {
        if (Physics.Raycast(raycastBot.position, transform.forward, out RaycastHit hit1, maxHeight, collisionLayer) 
            && Physics.Raycast(raycastTop.position, transform.forward, out RaycastHit hit2, maxHeight, collisionLayer))
        {
            if (hit1.transform.position.z == hit2.transform.position.z)
            {
                Vector3 newPos = transform.position;
                newPos.z = hit1.point.z - 0.2f;
                transform.position = newPos;
            }
            else
            {
                Vector3 newPos = transform.position;
                if (hit1.transform.position.z < hit2.transform.position.z)
                {
                    newPos.z = hit1.point.z - 0.2f;
                    transform.position = newPos;
                }
                else if (hit1.transform.position.z > hit2.transform.position.z)
                {
                    newPos.z = hit2.point.z - 0.2f;
                    transform.position = newPos;
                }
            }
        }

        Debug.DrawRay(raycastTop.position, transform.forward * maxHeight, Color.red);
        Debug.DrawRay(raycastBot.position, transform.forward * maxHeight, Color.red);
    }
}