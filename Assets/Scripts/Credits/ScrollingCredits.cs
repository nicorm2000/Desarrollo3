using System.Collections;
using UnityEngine;

public class ScrollingCredits : MonoBehaviour
{
    [Header("Scroll Configuration")]
    [SerializeField] private float scrollTime;
    [SerializeField] private AnimationCurve scrollSpeedCurve;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 targetPosition;

    [Header("Main Menu Button")]
    [SerializeField] private GameObject mainMenuButton;

    [Header("Objects Appearing")]
    [SerializeField] private float object1Time;
    [SerializeField] private float object2Time;
    [SerializeField] private float object3Time;
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;

    private void Start()
    {
        StartCoroutine(ScrollCredits());
    }

    private IEnumerator ScrollCredits()
    {
        float timer = 0f;

        while (timer < scrollTime)
        {
            float progress = timer / scrollTime;
            float scrollSpeed = scrollSpeedCurve.Evaluate(progress);

            transform.position = Vector3.Lerp(initialPosition, targetPosition, scrollSpeed);

            timer += Time.deltaTime;

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


            yield return null;
        }

        transform.position = targetPosition;
        mainMenuButton.SetActive(true);
    }
}