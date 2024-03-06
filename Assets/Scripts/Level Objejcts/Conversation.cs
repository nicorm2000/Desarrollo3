using System.Collections;
using UnityEngine;
using TMPro;

public class Conversation : MonoBehaviour
{
    [Header("Conversation Configuration")]
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float typingSpeed;
    [SerializeField] private float wordWait;
    [SerializeField] private string[] phrases;
    [SerializeField] private string[] phrasesBeforeBoss;
    private bool _isBeforeBossPhase = false;

    [Header("Layers to include")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Wave Manager Dependencies")]
    [SerializeField] private WaveManager waveManager;

    private void Start()
    {
        StartCoroutine(TypePhrases());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (((Constants.ONE << collision.gameObject.layer) & includeLayer) != Constants.ZERO && Time.timeScale != 0)
        {
            if (waveManager.currentWaveIndex == 14)
            {
                _isBeforeBossPhase = true;
            }

            StartCoroutine(TypePhrases());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (((Constants.ONE << collision.gameObject.layer) & includeLayer) != Constants.ZERO && Time.timeScale != 0)
        {
            StopAllCoroutines();
        }
    }


    private IEnumerator TypePhrases()
    {
        string[] selectedPhrases = _isBeforeBossPhase ? phrasesBeforeBoss : phrases;

        int index = 0;

        while (true)
        {
            string phrase = selectedPhrases[index];
            yield return TypePhrase(phrase);
            yield return new WaitForSeconds(wordWait);
            textMeshPro.text = "";

            index = (index + 1) % selectedPhrases.Length;
        }
    }

    private IEnumerator TypePhrase(string phrase)
    {
        for (int i = 0; i < phrase.Length; i++)
        {
            textMeshPro.text = phrase[..(i + 1)];
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}