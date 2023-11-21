using UnityEngine;
using System.Collections;

public class BBQController : MonoBehaviour
{
    [SerializeField] private GameObject[] fireObjects;
    [SerializeField] private float activationDuration = 2.0f;
    [SerializeField] private float timeBetweenActivations = 1.0f;
    [SerializeField] private float cooldownDuration = 10.0f;

    private bool _fireLoop = true;

    private void Start()
    {
        StartCoroutine(ActivateFireLoop());
    }

    private IEnumerator ActivateFireLoop()
    {
        while (_fireLoop)
        {
            for (int i = 0; i < fireObjects.Length; i++)
            {
                fireObjects[i].SetActive(true);

                float timer = 0f;
                while (timer < activationDuration)
                {
                    timer += Time.deltaTime;

                    if (timer >= timeBetweenActivations && i < fireObjects.Length - 1)
                    {
                        fireObjects[i + 1].SetActive(true);
                    }

                    yield return null;
                }

                fireObjects[i].SetActive(false);
            }

            yield return new WaitForSeconds(cooldownDuration);
        }
    }
}