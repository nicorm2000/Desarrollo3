using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Animation Curve")]
    [SerializeField] private AnimationCurve borderCurve;

    [Header("Images")]
    [SerializeField] private Image border1;
    [SerializeField] private Image border2;
    [SerializeField] private Image border3;

    [Header("Border Duration")]
    [SerializeField] private float borderColorDuration;

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
    /// Gradually changes the border color by lowering the alpha value using an animation curve.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator ChangeBorderColor()
    {
        border1.color = new Color(border1.color.r, border1.color.g, border1.color.b, 1.0f);
        border2.color = new Color(border2.color.r, border2.color.g, border2.color.b, 1.0f);
        border3.color = new Color(border3.color.r, border3.color.g, border3.color.b, 1.0f);

        Color initialColor = border1.color;
        float timer = 0f;

        border3.gameObject.SetActive(true);
        border1.gameObject.SetActive(true);
        border2.gameObject.SetActive(true);

        while (timer < borderColorDuration)
        {
            float progress = timer / borderColorDuration;
            float alpha = borderCurve.Evaluate(progress);

            Color currentColor = initialColor;
            currentColor.a = Mathf.Lerp(initialColor.a, 0f, alpha);

            border1.color = currentColor;
            border2.color = currentColor;
            border3.color = currentColor;

            yield return null;
            timer += Time.deltaTime;
        }

        border1.gameObject.SetActive(false);
        border2.gameObject.SetActive(false);
        border3.gameObject.SetActive(false);
    }
}