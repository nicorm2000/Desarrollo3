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

    private void SetMaxHealthText(float maxHealth)
    {
        _maxHealthText.text = "/" + maxHealth.ToString();
    }

    private void SetHealthText(float currentHealth)
    {
        _currentHealthText.text = currentHealth.ToString();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        SetMaxHealthText(health);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        SetHealthText(health);
    }

    public void SetMaxAndCurrentHealth(float maxHealth, float currentHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(currentHealth);
    }

    public IEnumerator ChangeBorderColor(Color color, float duration)
    {
        border.color = color;
        yield return new WaitForSeconds(duration);

        border.color = Color.white;
    }
}