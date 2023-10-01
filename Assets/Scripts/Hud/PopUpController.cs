using UnityEngine;

public class PopUpController : MonoBehaviour
{
    [SerializeField] private float initialYPosition = -5f;
    [SerializeField] private float finalYPosition = 5f;
    [SerializeField] private float popUpSpeed = 5f;

    private bool isPoppedUp = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isPoppedUp = true;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            isPoppedUp = false;
        }

        float targetY = isPoppedUp ? finalYPosition : initialYPosition;
        float newY = Mathf.MoveTowards(transform.position.y, targetY, popUpSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}