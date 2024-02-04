using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private bool isActive = false;

    public void Activate(float duration)
    {
        isActive = true;
        StartCoroutine(DeactivateAfter(duration));
    }

    public bool IsActive()
    {
        return isActive;
    }

    private IEnumerator DeactivateAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            Debug.Log("Player triggered an active mine!");
        }
    }
}