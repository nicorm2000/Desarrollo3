using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider slider;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData BossData;

    /// <summary>
    /// Sets the maximum health value and updates the UI slider and text.
    /// </summary>
    /// <param name="health">The maximum health value.</param>
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    /// <summary>
    /// Sets the current health value and updates the UI slider and text.
    /// </summary>
    /// <param name="health">The current health value.</param>
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxAndCurrentHealth(float maxHealth, float currentHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }
}