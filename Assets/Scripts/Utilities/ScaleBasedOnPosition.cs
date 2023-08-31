using UnityEngine;

public class ScaleBasedOnPosition : MonoBehaviour
{
    [SerializeField] private float maxYPosition = 10.0f;
    [SerializeField] private float minYPosition = -10.0f;
    [SerializeField] private float minScale = 0.75f;
    [SerializeField] private float maxScale = 1.25f;

    private void Update()
    {
        float yPosition = transform.position.y;
        float normalizedPosition = Mathf.InverseLerp(minYPosition, maxYPosition, yPosition);
        float targetScale = Mathf.Lerp(maxScale, minScale, normalizedPosition);

        transform.localScale = new Vector3(targetScale, targetScale, targetScale);
    }
}