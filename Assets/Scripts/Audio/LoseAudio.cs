using UnityEngine;

public class LoseAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string scream;
    [SerializeField] string grill;

    private void Start()
    {
        if (!AudioManager.mute)
        {
            audioManager.PlaySound(scream);
            audioManager.PlaySound(grill);
        }
    }
}