using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool muteSFX = false;
    public static bool muteMusic = false;
    public bool isPlaying = false;

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

    public void Mute()
    {
        muteSFX = !muteSFX;
        Debug.Log(muteSFX);

        StopSounds();

        //isPlaying = !isPlaying;
        //Debug.Log(isPlaying);
        /*if (!audioManager.isPlaying)
        {
            if (!AudioManager.mute)
            {
                audioManager.PlaySound(intro);
            }
            else
            {
                audioManager.StopSounds();
            }

            audioManager.isPlaying = true;
        }*/
    }
}