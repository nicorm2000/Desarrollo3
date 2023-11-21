using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("SFX Config")]
    [SerializeField] private Image sfxImage;
    [SerializeField] private Sprite sfxSpriteDefault;
    [SerializeField] private Sprite sfxSpriteSelected;
    public static bool muteSFX = false;
    public UnityEvent onSFXMute;
    public UnityEvent onSFXUnmute;
    
    [Header("Music Config")]
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite musicSpriteDefault;
    [SerializeField] private Sprite musicSpriteSelected;
    public static bool muteMusic = false;
    public UnityEvent onMusicMute;
    public UnityEvent onMusicUnmute;

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
            onSFXMute.Invoke();
        }
        else
        {
            sfxImage.sprite = sfxSpriteDefault;
            onSFXUnmute.Invoke();
        }
    }

    public void ToggleMusicMute()
    {
        muteMusic = !muteMusic;
        if (muteMusic)
        {
            musicImage.sprite = musicSpriteSelected;
            StopSounds();
            onMusicMute.Invoke();
        }
        else
        {
            musicImage.sprite = musicSpriteDefault;
            onMusicUnmute.Invoke();
        }
    }
}