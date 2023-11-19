using UnityEngine;

public class WinAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string laugh;
    [SerializeField] string grill;

    private void Start()
    {
        audioManager.PlaySound(laugh);
        audioManager.PlaySound(grill);
    }
}