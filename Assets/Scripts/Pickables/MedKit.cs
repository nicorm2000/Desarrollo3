using System.Collections;
using UnityEngine;

public class MedKit : MonoBehaviour, IPickable
{
    [Header("MedKit Configuration")]
    [SerializeField] private float healAmount;
    [SerializeField] private float coolDownTime;
    [SerializeField] private GameObject medkitIcon;

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

    public float CooldownTime => coolDownTime;

    public void ApplyEffect()
    {
        if (isActive)
        {
            Heal();
            isActive = false;
            ModifyVisuals(cooldownMaterial, false);
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

    public void ModifyVisuals(Material material, bool icon)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }

        medkitIcon.SetActive(icon);
    }

    public bool IsReadyForPickUp()
    {
        return isActive;
    }

    private void Heal()
    {
        if ((playerData.currentHealth += healAmount) > playerData.maxHealth)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
        else
        {
            playerData.currentHealth += healAmount;
        }
        playerHealthUI.SetHealth(playerData.currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            ApplyEffect();
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(coolDownTime);
        isActive = true;
        ModifyVisuals(activeMaterial, true);
    }
}