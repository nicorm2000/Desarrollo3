using System.Collections;
using UnityEngine;

public class TeleportPlayerToLevel : MonoBehaviour
{
    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;

    [Header("Bao Basket Inidicator Dependencies")]
    [SerializeField] private GameObject baoBasketIndicator;
    [SerializeField] private GameObject baoBasketIndicatorLogic;

    [Header("Audio Manager Dependencies")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string teleportButton;

    [Header("Wave Manager Dependencies")]
    [SerializeField] WaveManager waveManager;

    [Header("Enemies Movement Dependencies")]
    [SerializeField] private AIChase[] aIChase;
    [SerializeField] private AIShooterChase aIShooterChase;

    private readonly float _transitonOnTime = 1f;

    [Header("Teleport locations")]
    [SerializeField] private Transform levelSpawn;
    [SerializeField] private Transform bosLevelSpawn;

    [Header("Visual Dependencies")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject transitionOff;
    [SerializeField] private GameObject goToLevelText;
    [SerializeField] private GameObject goToBossLevelText;
    [SerializeField] private GameObject teleportTimer;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    public bool playerCanTeleport = false;
    public bool isPlayerOnTeleportArea = false;

    [Header("Camera Movement Dependences")]
    [SerializeField] private CameraMovement cameraMovement;

    [Header("Time to teleport player")]
    [SerializeField] private TeleportingTimer teleportingTimer;
    [SerializeField] private int timeToTeleportPlayer;

    [Header("Button animation")]
    [SerializeField] private Animator buttonAnimator;
    private bool _isPressed;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    private void OnTriggerEnter(Collider player)
    {
        if (((Constants.ONE << player.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            isPlayerOnTeleportArea = true;

            if (waveManager.currentWaveIndex <= Constants.MAX_WAVES)
            {
                goToLevelText.SetActive(true);
            }

            else
            {
                goToBossLevelText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (((Constants.ONE << player.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            isPlayerOnTeleportArea = false;
            playerCanTeleport = false;
            teleportTimer.SetActive(false);
            teleportingTimer.currentTime = teleportingTimer.maxTime;


            if (waveManager.currentWaveIndex <= Constants.MAX_WAVES)
            {
                goToLevelText.SetActive(false);
            }

            else
            {
                goToBossLevelText.SetActive(false);
            }
        }
    }

    public void CheckPlayerTeleportToLevel()
    {
        if (isPlayerOnTeleportArea)
        {
            teleportTimer.SetActive(true);
            _isPressed = true;
            HandleTeleportButtonChange(_isPressed);
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(teleportButton);
            }
            StartCoroutine(CheckTeleport(timeToTeleportPlayer));
        }
    }

    public IEnumerator PlayerTeleport(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        if (playerCanTeleport)
        {
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(playerData.pickUpWeapon);
            }

            if (waveManager.currentWaveIndex <= Constants.MAX_WAVES)
            {
                player.transform.position = levelSpawn.position;
            }

            else
            {
                player.transform.position = bosLevelSpawn.position;
            }

            StartCoroutine(increaseSizeOff.ActiveTransition(_transitonOnTime));
            StartCoroutine(increaseSizeOff.DisableTransition(_transitonOnTime));

            aIChase[0].EnemiesCanMove();
            aIChase[1].EnemiesCanMove();
            aIChase[2].EnemiesCanMove();
            aIChase[3].EnemiesCanMove();
            aIShooterChase.EnemyShooterCanMove();
            teleportTimer.SetActive(false);
        }

        playerCanTeleport = false;
    }

    private IEnumerator CheckTeleport(float timeToWait)
    {
        goToLevelText.SetActive(false);
        goToBossLevelText.SetActive(false);

        yield return new WaitForSeconds(timeToWait);

        playerData.haveAGun = true;

        if (isPlayerOnTeleportArea)
        {
            teleportTimer.SetActive(true);
            transitionOff.SetActive(false);
            StartCoroutine(increaseSizeOn.ActiveTransition(_transitonOnTime));
            StartCoroutine(increaseSizeOn.DisableTransition(_transitonOnTime));
            playerCanTeleport = true;
        }

        baoBasketIndicator.SetActive(false);
        baoBasketIndicatorLogic.SetActive(false);

        StartCoroutine(PlayerTeleport(_transitonOnTime));

        _isPressed = false;
        HandleTeleportButtonChange(_isPressed);
    }

    private void HandleTeleportButtonChange(bool isPressed)
    {
        buttonAnimator.SetBool("IsPressed", isPressed);
    }
}