using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void PlaySound(string audioEvent)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }
}