using System.Threading;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;

    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private GameObject pickUpWeaponText;
    [SerializeField] private ChangePlayerWeapon changePlayerWeapon;
    [SerializeField] private GameObject player;

    public PlayerData playerData;
    public WeaponData weaponData;

    public bool playerCanTeleport = false;
    public bool isPlayerOnTeleportArea = false;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void Update()
    {
        if(playerCanTeleport == true && increaseSizeOn.isTransitionFinish() == true) 
        {
            increaseSizeOff.ActiveTransition();
            PlayerTeleport();
            playerCanTeleport = false;
        }
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

    public void CheckPlayerTeleport()
    {
        playerData.haveAGun = true;
        increaseSizeOn.ActiveTransition();

        if (isPlayerOnTeleportArea == true)
        {
            playerCanTeleport = true;
        }
    }

    public void PlayerTeleport() 
    {
        changePlayerWeapon.ChangeWeapon(weaponData.weaponID);
        player.transform.position = levelSpawn.transform.position;
    }
}