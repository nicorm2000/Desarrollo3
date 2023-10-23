using UnityEngine;

public class ChangeWeaponSprite : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    public void ChangeWeapon(int spriteNumber) 
    {
        switch (spriteNumber) 
        {
            case 0:
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
            break; 
            
            case 1:
                weapons[0].SetActive(false);
                weapons[1].SetActive(true);
                weapons[2].SetActive(false);
            break;

            case 2:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(true);
                break; 
        }
    }
}