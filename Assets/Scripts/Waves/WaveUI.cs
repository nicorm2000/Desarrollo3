using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaveUI : MonoBehaviour
{
    [SerializeField] GameObject waveName;
    [SerializeField] GameObject waveCompleted;
    [SerializeField] TMP_Text waveText;

    public void ShowWaveText(int index)
    {
        waveText.text = "Wave: " + index.ToString();
    }

    public IEnumerator WaveShowUI(int index)
    {
        ShowWaveText(index);
        waveName.SetActive(true);
        waveName.GetComponent<TextMeshProUGUI>().text = "Wave: " + index.ToString();
        yield return new WaitForSeconds(3f);

        waveName.SetActive(false);
    }

    public IEnumerator WaveCompletedShowUI()
    {
        waveCompleted.SetActive(true);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(5);
    }
}