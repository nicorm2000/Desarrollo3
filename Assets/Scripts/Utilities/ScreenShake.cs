using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public float duration = 1.0f;

    public IEnumerator Shake()
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            float strength = animationCurve.Evaluate(elapsedTime / duration);
            transform.localPosition = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = startPosition;
    }
}