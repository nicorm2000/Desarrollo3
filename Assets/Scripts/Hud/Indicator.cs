using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [Header("Indicator Set Up")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private Transform target;

    [Header("Visuals")]
    [SerializeField] private Sprite offScreenSprite;
    [SerializeField] private Sprite onScreenSprite;    
    
    [Header("Visibility Area")]
    [SerializeField] private Collider visibilityCollider;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    private Vector3 _targetPosition;
    private RectTransform _indicatorRectTransform;
    private Image _indicatorImage;

    void Awake()
    {
        //Replace with target position
        _targetPosition = target.position;
        _indicatorRectTransform = indicator.GetComponent<RectTransform>();
        _indicatorImage = indicator.GetComponent<Image>();

        Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            Show();
            _indicatorImage.sprite = onScreenSprite;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            UpdateIndicatorPosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            Hide();
        }
    }

    private void UpdateIndicatorPosition()
    {
        float borderSize = 100f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(_targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            RotatePointerTowardsTargetPosition();
            _indicatorImage.sprite = offScreenSprite;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            _indicatorRectTransform.position = cappedTargetScreenPosition;
            _indicatorRectTransform.localPosition = new Vector3(_indicatorRectTransform.localPosition.x, _indicatorRectTransform.localPosition.y, 0f);
        }
        else
        {
            _indicatorImage.sprite = onScreenSprite;
            _indicatorRectTransform.position = targetPositionScreenPoint;
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
        indicator.SetActive(false);
    }

    private void Show()
    {
        indicator.SetActive(true);
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}