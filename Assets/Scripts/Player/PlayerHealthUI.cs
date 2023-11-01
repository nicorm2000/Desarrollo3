using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _currentHealthText;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Slider slider;

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

    public IEnumerator ChangeBorderColor(Color color)
    {
        border.color = color;
        yield return new WaitForSeconds(3f);

        border.color = Color.white;
    }
}