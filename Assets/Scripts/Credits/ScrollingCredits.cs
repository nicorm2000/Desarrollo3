using System.Collections;
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
    [SerializeField] private float object1Time;
    [SerializeField] private float object2Time;
    [SerializeField] private float object3Time;
    [SerializeField] private float object4Time;
    [SerializeField] private float object5Time;
    [SerializeField] private float mainMenuWhileCreditsTime;
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;
    [SerializeField] private GameObject object4;
    [SerializeField] private GameObject object5;
    [SerializeField] private GameObject mainMenuWhileCredits;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = gameObject.transform.position;
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

            if (progress >= object1Time)
            {
                object1.SetActive(true);
            }

            if (progress >= object2Time)
            {
                object2.SetActive(true);
            }

            if (progress >= object3Time)
            {
                object3.SetActive(true);
            }

            if (progress >= object4Time)
            {
                object4.SetActive(true);
            }

            if (progress >= object5Time)
            {
                object5.SetActive(true);
            }

            yield return null;
        }

        transform.position = targetPosition.position;
        mainMenuButton.SetActive(true);
    }
}