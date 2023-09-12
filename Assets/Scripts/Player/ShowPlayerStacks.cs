using TMPro;
using UnityEngine;

public class ShowPlayerStacks : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private TMP_Text speedText;

    [SerializeField] private TMP_Text damageText;

    [SerializeField] private Shop shopManager;
    void Start()
    {
        healthText.text = "Health: " + shopManager.playerHealth.ToString();
        speedText.text = "Speed: " + shopManager.playerSpeed.ToString();
        damageText.text = "Damage: " + shopManager.playerDamage.ToString();
    }

    void Update()
    {
        healthText.text = "Health: " + shopManager.playerHealth.ToString();
        speedText.text = "Speed: " + shopManager.playerSpeed.ToString();
        damageText.text = "Damage: " + shopManager.playerDamage.ToString();
    }
}
