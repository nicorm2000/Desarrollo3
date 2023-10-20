using System.Collections;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [SerializeField] private string[] phrases = { "Hey, this is the shop!", "Please, choose your next weapon!", "Let the carnage begin!" };
    [SerializeField] private bool animateText = true;
    [SerializeField] private TMPro.TextMeshProUGUI textMeshPro;
    [SerializeField] private float typingSpeed = 0.1f;

    private int currentIndex = 0;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(AnimateText());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopCoroutine(AnimateText());
        }
    }

    private IEnumerator AnimateText()
    {
        while (animateText)
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