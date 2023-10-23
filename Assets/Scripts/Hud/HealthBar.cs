using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _currentHealthText;

    public PlayerData playerData;

    public Slider slider;

    private void Start()
    {
        _maxHealthText.text = "/" + playerData.maxHealth.ToString();
        _currentHealthText.text = playerData.currentHealth.ToString();
    }

    private void Update()
    {
        _maxHealthText.text = "/" + playerData.maxHealth.ToString();
        _currentHealthText.text = playerData.currentHealth.ToString();
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