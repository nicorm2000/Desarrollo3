using TMPro;
using UnityEngine;
using System.Collections;

public class WaveUI : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject waveName;
    [SerializeField] GameObject waveCompleted;
    
    [Header("Text Mesh Pro")]
    [SerializeField] TMP_Text waveText;

    [Header("Animation Duration")]
    [SerializeField] private float waveShowDuration;
    [SerializeField] private float waveShowCompletedDuration;

    /// <summary>
    /// Displays the wave index on the waveText UI element.
    /// </summary>
    /// <param name="index">The index of the wave to display.</param>
    public void ShowWaveText(int index)
    {
        waveText.text = "Wave: " + index.ToString();
    }

    /// <summary>
    /// Shows the wave UI elements for a specified duration and then hides them.
    /// </summary>
    /// <param name="index">The index of the wave to display.</param>
    /// <returns>An IEnumerator for coroutine execution.</returns>
    public IEnumerator ShowWaveUI(int index)
    {
        ShowWaveText(index);
        waveName.SetActive(true);
        waveName.GetComponent<TextMeshProUGUI>().text = "Wave: " + index.ToString();
        yield return new WaitForSeconds(waveShowDuration);

        waveName.SetActive(false);
    }

    /// <summary>
    /// Shows the wave completed UI element for a specified duration and then loads the win scene.
    /// </summary>
    /// <returns>An IEnumerator for coroutine execution.</returns>
    public IEnumerator ShowWaveCompletedUI()
    {
        waveCompleted.SetActive(true);
        yield return new WaitForSeconds(waveShowCompletedDuration);
        waveCompleted.SetActive(false);

        //Waves finished

        //StartCoroutine(increaseSizeOn.ActiveTransition(timeToTurnOnTransition));
        //yield return new WaitForSeconds(timeToTurnOnTransition);
        //mySceneManager.LoadSceneByName(winScene);
    }
}