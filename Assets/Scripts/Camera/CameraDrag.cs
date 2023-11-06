using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Header("Drag Configuration")]
    [SerializeField] private float dragSpeed;
    [SerializeField] private Vector3 cameraPos;
    [SerializeField] private Vector3 cameraOrigin;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            cameraOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1))
        {
            cameraOrigin = cameraPos;
            return;
        }

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - cameraOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed * Time.deltaTime, 0, pos.y * dragSpeed * Time.deltaTime);

        transform.Translate(move, Space.World);
    }
}