using UnityEngine;

public class TutorialAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string tutorial;

    private void Start()
    {
        if (!AudioManager.mute)
        {
            audioManager.PlaySound(tutorial);
        }
    }
}