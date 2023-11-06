using System.Collections;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    [Header("Drag Configuration")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float returnSpeedTime;
    [SerializeField] private AnimationCurve returnAnimationCurve;

    private Vector3 initialPosition;
    private Vector3 releasePosition;
    private float timer = 0f;
    private bool isDragging = false;

    private Coroutine coroutine = null;

    private void Start()
    {
        initialPosition = transform.position;
        releasePosition = initialPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            releasePosition = transform.position;

            if (coroutine == null)
            {
                coroutine = StartCoroutine(ReturnToOriginalPosition());
            }
        }

        if (isDragging)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 movement = new Vector3(-mouseX, -mouseY, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        timer = 0;

        while (timer < returnSpeedTime)
        {
            float progress = timer / returnSpeedTime;
            float curveValue = returnAnimationCurve.Evaluate(progress);
            transform.position = Vector3.Lerp(releasePosition, initialPosition, curveValue);
            timer += Time.deltaTime;

            if (isDragging)
            {
                transform.position = initialPosition;
                yield return null;
            }

            yield return null;
        }

        transform.position = initialPosition;
        coroutine = null;
    }
}