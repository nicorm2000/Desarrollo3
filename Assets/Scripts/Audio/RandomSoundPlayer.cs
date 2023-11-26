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

    private void Start()
    {
        if (!AudioManager.muteMusic)
        {
            PlayRandomSound();
        }
    }

    private void OnEnable()
    {
        audioManager.onSFXUnmute.AddListener(PlayRandomSound);
    }

    private void OnDisable()
    {
        audioManager.onSFXUnmute.RemoveListener(PlayRandomSound);
    }

    private IEnumerator PlaySoundsCoroutine()
    {
        isCoroutineRunning = true;

        while (!AudioManager.muteMusic)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));
            string randomAudioId = audioEventsName[Random.Range(0, audioEventsName.Length)];
            audioManager.PlaySound(randomAudioId);
        }

        isCoroutineRunning = false;
    }

    private void PlayRandomSound()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(PlaySoundsCoroutine());
        }
    }
}