using UnityEngine;

public class TutorialAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string tutorial;

    private void Start()
    {
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(tutorial);
        }
    }
}