using System.Collections;
using UnityEngine;

public class FloorTentacle : MonoBehaviour
{
    [Header("Floor Tentacle Set Up")]
    [SerializeField] private GameObject gO;
    [SerializeField] private GameObject warningGO;

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
        warningGO.SetActive(true);
        yield return new WaitForSeconds(bossData.attack3WarningDisplay);
        warningGO.SetActive(false);
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