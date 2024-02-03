using UnityEngine;

public class Mine : MonoBehaviour
{
    private bool isActive = false;
    private float activationDuration = 2f;
    private float playerActivationDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            Invoke("ActivateMine", playerActivationDelay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited");
        }
    }

    private void ActivateMine()
    {
        Debug.Log("Mine activated");
        isActive = true;
        Invoke("DeactivateMine", activationDuration);
    }

    private void DeactivateMine()
    {
        Debug.Log("Mine deactivated");
        isActive = false;
    }
}