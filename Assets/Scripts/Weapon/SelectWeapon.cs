using System.Collections;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
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
    [SerializeField] private GameObject pickUpWeaponText;
    [SerializeField] private ChangePlayerWeapon changePlayerWeapon;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject transitionOff;

    public bool playerCanTeleport = false;
    public bool isPlayerOnTeleportArea = false;

    public PlayerData playerData;
    public WeaponData weaponData;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = true;
            pickUpWeaponText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = false;
            pickUpWeaponText.SetActive(false);
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
        aIChase[0].EnemiesCanMove();
        aIChase[1].EnemiesCanMove();
        //Add new enemies here
        //aIChase[2].EnemiesCanMove();
        //aIChase[3].EnemiesCanMove();
        aIShooterChase.EnemyShooterCanMove();
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
            changePlayerWeapon.ChangeWeapon(weaponData.weaponID);
            player.transform.position = levelSpawn.transform.position;
            StartCoroutine(increaseSizeOff.ActiveTransition(_transitonOnTime));
            StartCoroutine(increaseSizeOff.DisableTransition(_transitonOnTime));
        }

        playerCanTeleport = false;
    }
}