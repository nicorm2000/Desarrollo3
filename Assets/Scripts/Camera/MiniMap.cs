using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private GameObject map;

    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] private string miniMap;

    public bool isMapActive = false;

    public void ActivateMap()
    {
        isMapActive = true;

        map.SetActive(true);
        
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(miniMap);
        }
    }

    public void DeactivateMap()
    {
        isMapActive = false;

        map.SetActive(false);

        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(miniMap);
        }
    }
}