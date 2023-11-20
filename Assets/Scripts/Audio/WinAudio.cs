using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class WinAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string laugh;
    [SerializeField] string grill;

    private void Start()
    {
        if (!AudioManager.mute)
        {
            audioManager.PlaySound(laugh);
            audioManager.PlaySound(grill);
        }
    }
}