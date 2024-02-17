using System.Collections;
using UnityEngine;

public class FloorTentacle : MonoBehaviour
{
    [Header("Floor Tentacle Set Up")]
    [SerializeField] private GameObject gO;
    [SerializeField] private GameObject warningGO;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string warning;

    private bool isActive = false;

    public void Activate(float duration)
    {
        StartCoroutine(ActivateWithDelay(duration));
    }

    private IEnumerator ActivateWithDelay(float duration)
    {
        warningGO.SetActive(true);
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(warning);
        }
        yield return new WaitForSeconds(bossData.attack3WarningDisplay);
        warningGO.SetActive(false);
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
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO && isActive)
        {
            Debug.Log("Player damaged!");
        }
    }
}