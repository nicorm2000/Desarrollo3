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

    private void OnEnable()
    {
        audioManager.onSFXUnmute.AddListener(PlaySFX);
        audioManager.onMusicUnmute.AddListener(PlayMusic);
    }

    private void OnDisable()
    {
        audioManager.onSFXUnmute.RemoveListener(PlaySFX);
        audioManager.onMusicUnmute.RemoveListener(PlayMusic);
    }
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

    private void PlayMusic()
    {
        audioManager.PlaySound(music);
    }
}