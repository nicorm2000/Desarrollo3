using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void PlaySound(string audioEvent)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    public void StopSounds()
    {
        AkSoundEngine.StopAll();
    }

    //private static AudioManager instance;
    //public static AudioManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<AudioManager>();

    //            if (instance == null)
    //            {
    //                GameObject singleton = new GameObject("AudioManager");
    //                instance = singleton.AddComponent<AudioManager>();
    //            }
    //        }

    //        return instance;
    //    }
    //}

    //private AudioManager() { }

    //public void PlaySound(string audioEvent)
    //{
    //    AkSoundEngine.PostEvent(audioEvent, gameObject);
    //}

    //public void StopSounds()
    //{
    //    AkSoundEngine.StopAll();
    //}

    //private void OnApplicationQuit()
    //{
    //    instance = null;
    //}
}