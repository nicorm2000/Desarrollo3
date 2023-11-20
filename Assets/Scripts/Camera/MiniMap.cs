using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private GameObject map;

    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] private string miniMap;

    public bool isMapActive = true;

    public void ActivateMap()
    {
        isMapActive = false;

        map.SetActive(true);
        
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(miniMap);
        }
    }

    public void DeactivateMap()
    {
        isMapActive = true;

        map.SetActive(false);

        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(miniMap);
        }
    }
}