using UnityEngine;

public class CreditsAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string credits;

    private void Start()
    {
        if (!AudioManager.muteSFX)
        { 
            audioManager.PlaySound(credits);
        }
    }
}