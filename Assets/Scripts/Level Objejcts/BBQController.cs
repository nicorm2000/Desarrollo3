using UnityEngine;
using System.Collections;

public class BBQController : MonoBehaviour
{
    [SerializeField] private GameObject[] fireObjects; // Array of fire objects
    [SerializeField] private float activationDuration = 2.0f; // Duration each fire is active
    [SerializeField] private float timeBetweenActivations = 1.0f; // Time between activating the next fire
    [SerializeField] private float cooldownDuration = 10.0f; // Time to wait before restarting the loop

    private bool _fireLoop = true;

    private void Start()
    {
        // Start the loop coroutine
        StartCoroutine(ActivateFireLoop());
    }

    private IEnumerator ActivateFireLoop()
    {
        while (_fireLoop)
        {
            for (int i = 0; i < fireObjects.Length; i++)
            {
                // Activate the current fire object
                fireObjects[i].SetActive(true);

                float timer = 0f;
                while (timer < activationDuration)
                {
                    timer += Time.deltaTime;

                    // Check if it's time to spawn the next fire object
                    if (timer >= timeBetweenActivations && i < fireObjects.Length - 1)
                    {
                        // Activate the next fire object
                        fireObjects[i + 1].SetActive(true);
                    }

                    yield return null; // Wait for the next frame
                }

                // Deactivate the current fire object
                fireObjects[i].SetActive(false);
            }

            // Wait for the cooldown duration before restarting the loop
            yield return new WaitForSeconds(cooldownDuration);

            // Ensure the first fire object is off before starting the loop again
            fireObjects[0].SetActive(false);
        }
    }
}
