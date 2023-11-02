using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image border;

    [Header("Text Mesh Pro")]
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _currentHealthText;

    [Header("Sliders")]
    [SerializeField] private Slider slider;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

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

    /// <summary>
    /// Sets both the maximum and current health values and updates the UI slider and text.
    /// </summary>
    /// <param name="maxHealth">The maximum health value.</param>
    /// <param name="currentHealth">The current health value.</param>
    public void SetMaxAndCurrentHealth(float maxHealth, float currentHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }

    /// <summary>
    /// Changes the border color of the UI element for a specified duration.
    /// </summary>
    /// <param name="color">The target color for the border.</param>
    /// <param name="duration">The duration of the color change.</param>
    public IEnumerator ChangeBorderColor(Color color, float duration)
    {
        border.color = color;
        yield return new WaitForSeconds(duration);

        border.color = Color.white;
    }
}