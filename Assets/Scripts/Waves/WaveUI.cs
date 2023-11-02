using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    [Header("Win Scene")]
    [SerializeField] private int winScene;

    public void ShowWaveText(int index)
    {
        waveText.text = "Wave: " + index.ToString();
    }

    public IEnumerator WaveShowUI(int index)
    {
        ShowWaveText(index);
        waveName.SetActive(true);
        waveName.GetComponent<TextMeshProUGUI>().text = "Wave: " + index.ToString();
        yield return new WaitForSeconds(waveShowDuration);

        waveName.SetActive(false);
    }

    public IEnumerator WaveCompletedShowUI()
    {
        waveCompleted.SetActive(true);
        yield return new WaitForSeconds(waveShowCompletedDuration);

        SceneManager.LoadScene(winScene);
    }
}