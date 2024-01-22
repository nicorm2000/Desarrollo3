using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [Header("Indicator Set Up")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private Camera indicatorCamera;
    [SerializeField] private Sprite offScreenSprite;
    [SerializeField] private Sprite onScreenSprite;

    private Vector3 _targetPosition;
    private RectTransform _indicatorRectTransform;
    private Image _indicatorImage;

    void Awake()
    {
        _targetPosition = new Vector3(0, 10);
        _indicatorRectTransform = indicator.GetComponent<RectTransform>();
        _indicatorImage = indicator.GetComponent<Image>();
    }

    private void Update()
    {
        float borderSize = 100f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(_targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            RotatePointerTowardsTargetPosition();
            _indicatorImage.sprite = offScreenSprite;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            if (cappedTargetScreenPosition.x <= borderSize)
                cappedTargetScreenPosition.x = 0f;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
                cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize)
                cappedTargetScreenPosition.y = 0f;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
                cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 indicatorWorldPosition = indicatorCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            _indicatorRectTransform.position = indicatorWorldPosition;
            _indicatorRectTransform.localPosition = new Vector3(_indicatorRectTransform.localPosition.x, _indicatorRectTransform.localPosition.y, 0f);
        }
        else 
        {
            _indicatorImage.sprite = onScreenSprite;
            Vector3 indicatorWorldPosition = indicatorCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            _indicatorRectTransform.position = indicatorWorldPosition;
            _indicatorRectTransform.localPosition = new Vector3(_indicatorRectTransform.localPosition.x, _indicatorRectTransform.localPosition.y, 0f);


            _indicatorRectTransform.localEulerAngles = Vector3.zero;
        }
    }

    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 toPosition = _targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;

        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        float angle = GetAngleFromVectorFloat(dir);

        _indicatorRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show(Vector3 targetPosition)
    {
        gameObject.SetActive(true);

        this._targetPosition = targetPosition;
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}