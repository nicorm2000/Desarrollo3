using System.Collections;
using UnityEngine;

public class FloorTentacle : MonoBehaviour
{
    [Header("Floow Tentacle Set Up")]
    [SerializeField] private GameObject gO;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    private bool isActive = false;

    public void Activate(float duration)
    {
        StartCoroutine(ActivateWithDelay(duration));
    }

    private IEnumerator ActivateWithDelay(float duration)
    {
        Debug.Log("Delay Start");
        yield return new WaitForSeconds(bossData.attack3WarningDisplay);
        Debug.Log("Delay Finish");
        isActive = true;
        gO.SetActive(isActive);
        Debug.Log(gameObject.name);
        StartCoroutine(DeactivateAfter(duration));
    }

    public bool IsFloorTentacleActive()
    {
        return isActive;
    }

    private IEnumerator DeactivateAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        isActive = false;
        gO.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            Debug.Log("Player damaged!");
        }
    }
}