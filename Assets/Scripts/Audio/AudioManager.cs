using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static bool muteSFX = false;
    public static bool muteMusic = false;
    public UnityEvent onMute;
    public UnityEvent onUnmute;

    public void PlaySound(string audioEvent)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    public void PlaySound(string audioEvent, GameObject gameObject)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    public void StopSounds()
    {
        AkSoundEngine.StopAll();
    }

    public void ToggleMute()
    {
        muteSFX = !muteSFX;

        if (muteSFX)
        {
            StopSounds();
            onMute.Invoke();
        }
        else
        {
            onUnmute.Invoke();
        }
    }
}