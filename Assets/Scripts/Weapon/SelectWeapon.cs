using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Teleport Trasnform")]
    [SerializeField] private Transform levelSpawn;

    [Header("Player Trasnform")]
    [SerializeField] private Transform player;

    [Header("Selec tWeapon UI Dependencies")]
    [SerializeField] private SelectWeaponUI selectWeaponUI;

    [Header("Change Player Weapon Dependencies")]
    [SerializeField] private ChangePlayerWeapon changePlayerWeapon;

    [Header("Weapon Data Dependencies")]
    [SerializeField] private WeaponData weaponData;

    /// <summary>
    /// Called when another collider enters the trigger.
    /// Sets the weapon selection text on the UI if the entering collider's layer is included in the specified layer mask.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            selectWeaponUI.SetWeaponSelectionText(true);
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger.
    /// Sets the weapon selection text on the UI to false if the exiting collider's layer is included in the specified layer mask.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            selectWeaponUI.SetWeaponSelectionText(false);
        }
    }

    /// <summary>
    /// Teleports the player to a specified position and changes their weapon.
    /// </summary>
    public void PlayerTeleport()
    {
        changePlayerWeapon.ChangeWeapon(weaponData.weaponID);
        player.transform.position = levelSpawn.position;
    }
}