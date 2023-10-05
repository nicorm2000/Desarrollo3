using TMPro;
using UnityEngine;

public class ShowPlayerStacks : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private TMP_Text speedText;

    [SerializeField] private TMP_Text damageText;

    public PlayerData playerData;

    void Start()
    {
        healthText.text = "Health: " + playerData.healthStackID.ToString();
        speedText.text = "Speed: " + playerData.speedStackID.ToString();
        damageText.text = "Damage: " + playerData.damageStackID.ToString();
    }

    void Update()
    {
        healthText.text = "Health: " + playerData.healthStackID.ToString();
        speedText.text = "Speed: " + playerData.speedStackID.ToString();
        damageText.text = "Damage: " + playerData.damageStackID.ToString();
    }
}
