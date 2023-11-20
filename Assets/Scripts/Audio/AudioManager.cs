using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool mute = false;

    public void PlaySound(string audioEvent)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    public void StopSounds()
    {
        AkSoundEngine.StopAll();
    }

    public void Mute()
    {
        mute = !mute;
        Debug.Log(mute);
    }
}