using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [Header("Text Mesh Pro")]
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _currentHealthText;

    [Header("Sliders")]
    [SerializeField] private Slider slider;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData BossData;

    /// <summary>
    /// Sets the maximum health text display.
    /// </summary>
    /// <param name="maxHealth">The maximum health value.</param>
    private void SetMaxHealthText(float maxHealth)
    {
        _maxHealthText.text = "/" + maxHealth.ToString();
    }

    /// <summary>
    /// Sets the current health text display.
    /// </summary>
    /// <param name="currentHealth">The current health value.</param>
    private void SetHealthText(float currentHealth)
    {
        _currentHealthText.text = currentHealth.ToString();
    }

    /// <summary>
    /// Sets the maximum health value and updates the UI slider and text.
    /// </summary>
    /// <param name="health">The maximum health value.</param>
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        SetMaxHealthText(health);
    }

    /// <summary>
    /// Sets the current health value and updates the UI slider and text.
    /// </summary>
    /// <param name="health">The current health value.</param>
    public void SetHealth(float health)
    {
        slider.value = health;

        SetHealthText(health);
    }
}