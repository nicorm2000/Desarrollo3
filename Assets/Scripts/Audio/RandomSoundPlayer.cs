using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [Header("Audio Configuration")]
    [SerializeField] private string[] audioEventsName;
    [SerializeField] private float minTimeBetweenSounds;
    [SerializeField] private float maxTimeBetweenSounds;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;

    private bool isCoroutineRunning = false;

    /// <summary>
    /// Start is called before the first frame update. Plays a random sound if SFX is not muted.
    /// </summary>
    private void Start()
    {
        if (!AudioManager.muteSFX)
        {
            PlayRandomSound();
        }
    }

    /// <summary>
    /// Called when the object becomes enabled and registers PlayRandomSound method to the SFX unmute event.
    /// </summary>
    private void OnEnable()
    {
        audioManager.onSFXUnmute.AddListener(PlayRandomSound);
    }

    /// <summary>
    /// Called when the object becomes disabled and removes PlayRandomSound method from the SFX unmute event.
    /// </summary>
    private void OnDisable()
    {
        audioManager.onSFXUnmute.RemoveListener(PlayRandomSound);
    }

    /// <summary>
    /// Coroutine that plays random sounds with a delay between them until SFX is muted.
    /// </summary>
    private IEnumerator PlaySoundsCoroutine()
    {
        isCoroutineRunning = true;

        while (!AudioManager.muteSFX)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));
            string randomAudioId = audioEventsName[Random.Range(0, audioEventsName.Length)];

            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(randomAudioId);
            }
        }

        isCoroutineRunning = false;
    }

    /// <summary>
    /// Plays a random sound by starting the PlaySoundsCoroutine if it is not already running.
    /// </summary>
    private void PlayRandomSound()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(PlaySoundsCoroutine());
        }
    }
}