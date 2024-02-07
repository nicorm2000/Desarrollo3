using UnityEngine;

public class ChangePlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    [Header("Gun OverHeat Dependeces")]
    [SerializeField] private GunOverheat[] gunOverheat;
    [SerializeField] private GameObject overHeatText;
    [SerializeField] private GameObject overHeatEffect;

    public void ChangeWeapon(int spriteNumber) 
    {
        switch (spriteNumber) 
        {
            case 0:
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[3].SetActive(false);
                weapons[4].SetActive(false);
                weapons[5].SetActive(false);
                weapons[6].SetActive(false);
                DesactiveOverHeatEffect();
                break; 
            
            case 1:
                weapons[0].SetActive(false);
                weapons[1].SetActive(true);
                weapons[2].SetActive(false);
                weapons[3].SetActive(false);
                weapons[4].SetActive(false);
                weapons[5].SetActive(false);
                weapons[6].SetActive(false);
                DesactiveOverHeatEffect();
                break;

            case 2:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(true);
                weapons[3].SetActive(false);
                weapons[4].SetActive(false);
                weapons[5].SetActive(false);
                weapons[6].SetActive(false);
                DesactiveOverHeatEffect();
                break; 

            case 3:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[3].SetActive(true);
                weapons[4].SetActive(false);
                weapons[5].SetActive(false);
                weapons[6].SetActive(false);
                DesactiveOverHeatEffect();
                break;

            case 4:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[3].SetActive(false);
                weapons[4].SetActive(true);
                weapons[5].SetActive(false);
                weapons[6].SetActive(false);
                DesactiveOverHeatEffect();
                break;

            case 5:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[3].SetActive(false);
                weapons[4].SetActive(false);
                weapons[5].SetActive(true);
                weapons[6].SetActive(false);
                DesactiveOverHeatEffect();
                break;

            case 6:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[3].SetActive(false);
                weapons[4].SetActive(false);
                weapons[5].SetActive(false);
                weapons[6].SetActive(true);
                DesactiveOverHeatEffect();
                break;
        }
    }

    private void DesactiveOverHeatEffect() 
    {
        for (int i = 0; i < gunOverheat.Length; i++)
        {
            gunOverheat[i]._currentOverheat = 0;
            gunOverheat[i]._overHeat = false;
        }

        overHeatText.SetActive(false);
        overHeatEffect.SetActive(false);
    }
}