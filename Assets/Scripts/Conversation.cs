using System.Collections;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.1f;

    private string[] phrases = { "Hey, this is the shop!", "Please, choose your next weapon!", "Let the carnage begin!" };
    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        while (true)
        {
            string currentPhrase = phrases[currentIndex];
            textMeshPro.text = "";

            for (int i = 0; i < currentPhrase.Length; i++)
            {
                textMeshPro.text += currentPhrase[i];
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(2f);
            currentIndex = (currentIndex + 1) % phrases.Length;
        }
    }
}