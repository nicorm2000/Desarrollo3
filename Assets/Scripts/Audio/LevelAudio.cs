using UnityEngine;

public class LevelAudio : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string conversationL;
    [SerializeField] private GameObject conversationLGO;
    [SerializeField] private string conversationR;
    [SerializeField] private GameObject conversationRGO;
    [SerializeField] private string grill;
    [SerializeField] private GameObject grillLGO;
    [SerializeField] private GameObject grillRGO;
    [SerializeField] private string conveyorBelt;
    [SerializeField] private GameObject[] conveyorBeltGO;
    [SerializeField] private string music;

    private void Start()
    {
        if (!AudioManager.muteSFX)
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

    //private void OnEnable()
    //{
    //    if (!AudioManager.muteSFX)
    //    {
    //        audioManager.PlaySound(conversationL, conversationLGO);
    //        audioManager.PlaySound(conversationR, conversationRGO);
    //        audioManager.PlaySound(grill, grillLGO);
    //        audioManager.PlaySound(grill, grillRGO);
    //        for (int i = 0; i < conveyorBeltGO.Length; i++)
    //        {
    //            audioManager.PlaySound(conveyorBelt, conveyorBeltGO[i]);
    //        }
    //    }
    //}

    private void OnDisable()
    {
        audioManager.StopSounds();
    }
}