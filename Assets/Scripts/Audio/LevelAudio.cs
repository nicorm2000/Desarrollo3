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
            PlaySounds();
        }
    }

    private void OnEnable()
    {
        audioManager.onUnmute.AddListener(PlaySounds);
    }

    private void OnDisable()
    {
        audioManager.onUnmute.RemoveListener(PlaySounds);
    }
    private void PlaySounds()
    {
        audioManager.PlaySound(conversationL, conversationLGO);
        audioManager.PlaySound(conversationR, conversationRGO);
        audioManager.PlaySound(grill, grillLGO);
        audioManager.PlaySound(grill, grillRGO);
        for (int i = 0; i < conveyorBeltGO.Length; i++)
        {
            audioManager.PlaySound(conveyorBelt, conveyorBeltGO[i]);
        }
        audioManager.PlaySound(music);
    }
}