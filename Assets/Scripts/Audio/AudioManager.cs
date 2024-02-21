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

    /// <summary>
    /// Start is called before the first frame update. Sets the appropriate sprite for the SFX and music buttons based on the mute state.
    /// </summary>
    private void Start()
    {
        if (sfxImage != null)
        {
            if (muteSFX)
            {
                sfxImage.sprite = sfxSpriteSelected;
            }
            else
            {
                sfxImage.sprite = sfxSpriteDefault;
            }
        }

        if (musicImage != null)
        {
            if (muteMusic)
            {
                musicImage.sprite = musicSpriteSelected;
            }
            else
            {
                musicImage.sprite = musicSpriteDefault;
            }
        }
    }

    /// <summary>
    /// Plays a sound using the specified audio event and game object.
    /// </summary>
    /// <param name="audioEvent">The name of the audio event to play.</param>
    public void PlaySound(string audioEvent)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    /// <summary>
    /// Plays a sound using the specified audio event and game object.
    /// </summary>
    /// <param name="audioEvent">The name of the audio event to play.</param>
    /// <param name="gameObject">The game object associated with the sound.</param>
    public void PlaySound(string audioEvent, GameObject gameObject)
    {
        AkSoundEngine.PostEvent(audioEvent, gameObject);
    }

    /// <summary>
    /// Stops all currently playing sounds.
    /// </summary>
    public void StopSounds()
    {
        AkSoundEngine.StopAll();
    }

    /// <summary>
    /// Stop playing specific sound.
    /// </summary>
    public void StopSpecificSound(string audioEvent, GameObject gameObject)
    {
        AkSoundEngine.StopPlayingID(AkSoundEngine.PostEvent(audioEvent, gameObject));
    }

    /// <summary>
    /// Toggles the mute state for SFX. Updates the sprite and invokes the appropriate events.
    /// </summary>
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

    /// <summary>
    /// Toggles the mute state for music. Updates the sprite, stops all sounds, and invokes the appropriate events.
    /// </summary>
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