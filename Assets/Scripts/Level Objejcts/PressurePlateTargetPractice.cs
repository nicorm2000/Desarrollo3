using System.Collections;
using UnityEngine;

public class PressurePlateTargetPractice : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float activationTime;
    [SerializeField] private float deactivationTime;
    [SerializeField] private GameObject[] targetList1;
    [SerializeField] private GameObject[] targetList2;
    [SerializeField] private GameObject[] targetList3;

    [Header("Camera Movement Dependencies")]
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AnimationCurve transitionCurve;
    [SerializeField] private float transitionDuration;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Audio Manager Dependencies")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string dummyCountdown;

    private Coroutine activationCoroutine;

    private Animator pressurePlateAnimator;
    private bool _isPressed;

    private void Start()
    {
        pressurePlateAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            _isPressed = true;
            HandlePressurePlateChange(_isPressed);

            cameraMovement.AimPractiveActivator(true);

            activationCoroutine ??= StartCoroutine(ActivateTargetsAfterDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            cameraMovement.AimPractiveActivator(false);

            if (activationCoroutine != null)
            {
                StopCoroutine(activationCoroutine);
                activationCoroutine = null;
            }

            _isPressed = false;
            HandlePressurePlateChange(_isPressed);

            StartCoroutine(TurnOffAllTargets());
        }
    }

    private IEnumerator ActivateTargetsAfterDelay()
    {
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(dummyCountdown);
        }
        yield return new WaitForSeconds(activationTime);

        ActivateTargets();
        activationCoroutine = null;
    }

    private void ActivateTargets()
    {
        ActivateRandomTarget(targetList1);
        ActivateRandomTarget(targetList2);
        ActivateRandomTarget(targetList3);
    }

    private void ActivateRandomTarget(GameObject[] targets)
    {
        if (targets.Length > 0)
        {
            int randomIndex = Random.Range(0, targets.Length);
            targets[randomIndex].SetActive(true);
        }
    }

    private IEnumerator TurnOffAllTargets()
    {
        yield return new WaitForSeconds(deactivationTime);

        TurnOffTargets(targetList1);
        TurnOffTargets(targetList2);
        TurnOffTargets(targetList3);
    }

    private void TurnOffTargets(GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            target.SetActive(false);
        }
    }

    private void HandlePressurePlateChange(bool isPressed)
    {
        pressurePlateAnimator.SetBool("IsPressed", isPressed);
    }
}