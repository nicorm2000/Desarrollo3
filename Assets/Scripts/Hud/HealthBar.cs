using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _currentHealthText;

    public PlayerData playerData;
    public Slider slider;

    private void SetHealthText(float maxHealth, float currentHealth)
    {
        _maxHealthText.text = "/" + maxHealth.ToString();
        _currentHealthText.text = currentHealth.ToString();
    }

    public void SetMaxHealth(float health) 
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health) 
    {
        slider.value = health;
    }
}