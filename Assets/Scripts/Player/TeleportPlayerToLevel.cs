using System.Collections;
using UnityEngine;

public class TeleportPlayerToLevel : MonoBehaviour
{
    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;

    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;

    [Header("Enemies Movement Dependencies")]
    [SerializeField] private AIChase[] aIChase;
    [SerializeField] private AIShooterChase aIShooterChase;

    private float _transitonOnTime = 1f;

    [Header("Visual Dependencies")]
    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject transitionOff;
    [SerializeField] private GameObject goToLevelText;

    public bool playerCanTeleport = false;
    public bool isPlayerOnTeleportArea = false;

    public PlayerData playerData;

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = true;
            goToLevelText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = false;
            goToLevelText.SetActive(false);
        }
    }

    public void CheckPlayerTeleportToLevel()
    {
        playerData.haveAGun = true;

        if (isPlayerOnTeleportArea == true)
        {
            transitionOff.SetActive(false);
            StartCoroutine(increaseSizeOn.ActiveTransition(_transitonOnTime));
            StartCoroutine(increaseSizeOn.DisableTransition(_transitonOnTime));
            playerCanTeleport = true;
        }

        StartCoroutine(PlayerTeleport(_transitonOnTime));
    }

    public IEnumerator PlayerTeleport(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        if (playerCanTeleport == true)
        {
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(playerData.pickUpWeapon);
            }

            player.transform.position = levelSpawn.transform.position;
            StartCoroutine(increaseSizeOff.ActiveTransition(_transitonOnTime));
            StartCoroutine(increaseSizeOff.DisableTransition(_transitonOnTime));

            aIChase[0].EnemiesCanMove();
            aIChase[1].EnemiesCanMove();
            //Add new enemies here
            //aIChase[2].EnemiesCanMove();
            //aIChase[3].EnemiesCanMove();
            aIShooterChase.EnemyShooterCanMove();
        }

        playerCanTeleport = false;
    }
}