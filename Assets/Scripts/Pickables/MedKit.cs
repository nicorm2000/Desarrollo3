using System.Collections;
using UnityEngine;

public class MedKit : MonoBehaviour, IPickable
{
    [Header("MedKit Configuration")]
    [SerializeField] private float healAmount;
    [SerializeField] private float coolDownTime;

    [Header("Materials Dependencies")]
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material cooldownMaterial;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Player Health UI Dependencies")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

    private bool isActive = true;

    #region INTERFACE_VARIABLES
    public float CooldownTime => coolDownTime;
    #endregion

    #region INTERFACE_METHODS
    public void ApplyEffect()
    {
        if (isActive)
        {
            playerData.currentHealth += healAmount;
            playerHealthUI.SetHealth(playerData.currentHealth);
            isActive = false;
            ModifyVisuals(cooldownMaterial);
            StartCooldown();
        }
    }

    public void StartCooldown()
    {
        if (!isActive)
        {
            StartCoroutine(CooldownCoroutine());
        }
    }

    public void ModifyVisuals(Material material)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    public bool IsReadyForPickUp()
    {
        return isActive;
    }
    #endregion

    #region UNITY_METHODS
    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO && playerData.currentHealth < playerData.maxHealth)
        {
            ApplyEffect();
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(coolDownTime);
        isActive = true;
        ModifyVisuals(activeMaterial);
    }
    #endregion
}