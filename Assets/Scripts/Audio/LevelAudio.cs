using UnityEngine;

public class LevelAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    
    [Header("Conversation")]
    [SerializeField] private string conversationL;
    [SerializeField] private GameObject conversationLGO;
    [SerializeField] private string conversationR;
    [SerializeField] private GameObject conversationRGO;
    
    [Header("Grill")]
    [SerializeField] private string grill;
    [SerializeField] private GameObject grillLGO;
    [SerializeField] private GameObject grillRGO;
    
    [Header("Conveyor Belt")]
    [SerializeField] private string conveyorBelt;
    [SerializeField] private GameObject[] conveyorBeltGO;
    
    [Header("Music")]
    [SerializeField] private string music;

    /// <summary>
    /// Start is called before the first frame update. Plays SFX and music if they are not muted.
    /// </summary>
    private void Start()
    {
        if (!AudioManager.muteSFX)
        {
            PlaySFX();
        }
        if (!AudioManager.muteMusic)
        {
            PlayMusic();
        }
    }

    /// <summary>
    /// Called when the object becomes enabled and registers PlaySFX and PlayMusic methods to the SFX and music unmute events, respectively.
    /// </summary>
    private void OnEnable()
    {
        audioManager.onSFXUnmute.AddListener(PlaySFX);
        audioManager.onMusicUnmute.AddListener(PlayMusic);
    }

    /// <summary>
    /// Called when the object becomes disabled and removes PlaySFX and PlayMusic methods from the SFX and music unmute events, respectively.
    /// </summary>
    private void OnDisable()
    {
        audioManager.onSFXUnmute.RemoveListener(PlaySFX);
        audioManager.onMusicUnmute.RemoveListener(PlayMusic);
    }

    /// <summary>
    /// Plays various SFX sounds, including conversations, grill sounds, and conveyor belt sounds.
    /// </summary>
    private void PlaySFX()
    {
        audioManager.PlaySound(conversationL, conversationLGO);
        audioManager.PlaySound(conversationR, conversationRGO);
        audioManager.PlaySound(grill, grillLGO);
        audioManager.PlaySound(grill, grillRGO);
        for (int i = 0; i < conveyorBeltGO.Length; i++)
        {
            audioManager.PlaySound(conveyorBelt, conveyorBeltGO[i]);
        }
    }

    /// <summary>
    /// Plays the music sound.
    /// </summary>
    private void PlayMusic()
    {
        audioManager.PlaySound(music);
    }
}