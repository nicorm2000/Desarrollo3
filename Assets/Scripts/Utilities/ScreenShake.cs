using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [Header("Animation Configuration")]
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float duration;

    /// <summary>
    /// Performs a shake effect over a specified duration.
    /// </summary>
    /// <returns>An enumerator for the shake effect.</returns>
    public IEnumerator Shake()
    {
        Vector3 startPosition = GetStartPosition();
        float elapsedTime = Constants.ZERO_F;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            float strength = CalculateStrength(elapsedTime);
            ApplyOffset(startPosition, strength);
            yield return null;
        }

        ResetPosition(startPosition);
    }

    /// <summary>
    /// Calculates the strength of the shake based on the elapsed time.
    /// </summary>
    /// <param name="elapsedTime">The elapsed time since the shake started.</param>
    /// <returns>The strength of the shake.</returns>
    private float CalculateStrength(float elapsedTime)
    {
        return animationCurve.Evaluate(elapsedTime / duration);
    }

    /// <summary>
    /// Applies the offset to the transform based on the starting position and strength.
    /// </summary>
    /// <param name="startPosition">The starting position of the transform.</param>
    /// <param name="strength">The strength of the shake.</param>
    private void ApplyOffset(Vector3 startPosition, float strength)
    {
        transform.localPosition = startPosition + Random.insideUnitSphere * strength;
    }

    /// <summary>
    /// Retrieves the starting position of the transform.
    /// </summary>
    /// <returns>The starting position of the transform.</returns>
    private Vector3 GetStartPosition()
    {
        return transform.localPosition;
    }

    /// <summary>
    /// Resets the position of the transform to the starting position.
    /// </summary>
    /// <param name="startPosition">The starting position of the transform.</param>
    private void ResetPosition(Vector3 startPosition)
    {
        transform.localPosition = startPosition;
    }
}