using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCredits : MonoBehaviour
{
    [Header("Scroll Configuration")]
    [SerializeField] private float scrollTime;
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private AnimationCurve scrollSpeedCurve;

    [Header("Main Menu Button")]
    [SerializeField] private GameObject mainMenuButton;

    [Header("Objects Appearing")]
    [SerializeField] private float[] objectTime;
    [SerializeField] private GameObject[] objects;

    [Header("Menu Button While Credits")]
    [SerializeField] private float mainMenuWhileCreditsTime;
    [SerializeField] private GameObject mainMenuWhileCredits;

    private Dictionary<float, GameObject> objectsToActivate;

    private void Start()
    {
        objectsToActivate = new Dictionary<float, GameObject>();

        for (int i = 0; i < objectTime.Length; i++)
        {
            objectsToActivate.Add(objectTime[i], objects[i]);
        }

        StartCoroutine(ScrollCredits());
    }

    private IEnumerator ScrollCredits()
    {
        float timer = 0f;

        while (timer < scrollTime)
        {
            float progress = timer / scrollTime;
            float scrollSpeed = scrollSpeedCurve.Evaluate(progress);

            transform.position = Vector3.Lerp(initialPosition.position, targetPosition.position, scrollSpeed);
            timer += Time.deltaTime;

            if (progress <= mainMenuWhileCreditsTime)
            {
                mainMenuWhileCredits.SetActive(true);
            }

            foreach (var kvp in objectsToActivate)
            {
                if (progress >= kvp.Key)
                {
                    kvp.Value.SetActive(true);
                }
            }

            yield return null;
        }

        transform.position = targetPosition.position;
        mainMenuButton.SetActive(true);
    }
}