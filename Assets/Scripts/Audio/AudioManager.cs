using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool mute = false;
    public bool isPlaying = false;

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