using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string intro;

    private void Start()
    {
        if (!AudioManager.muteMusic)
        {
            PlayMusic();
        }
    }

    private void OnEnable()
    {
        audioManager.onMusicUnmute.AddListener(PlayMusic);
    }

    private void OnDisable()
    {
        audioManager.onMusicUnmute.RemoveListener(PlayMusic);
    }

    private void PlayMusic()
    {
        audioManager.PlaySound(intro);
    }
}