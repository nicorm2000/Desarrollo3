using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Image sfxImage;
    [SerializeField] private Sprite sfxSpriteDefault;
    [SerializeField] private Sprite sfxSpriteSelected;

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
            sfxImage.sprite = sfxSpriteSelected;
            StopSounds();
            onMute.Invoke();
        }
        else
        {
            sfxImage.sprite = sfxSpriteDefault;
            onUnmute.Invoke();
        }
    }
}