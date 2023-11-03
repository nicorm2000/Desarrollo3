using UnityEngine;

public class SelectWeaponUI : MonoBehaviour
{
    [Header("Pick Up Text")]
    [SerializeField] private GameObject pickUpWeaponText;

    /// <summary>
    /// Sets the visibility of the weapon selection text on the UI.
    /// </summary>
    /// <param name="value">The visibility state (true or false) of the weapon selection text.</param>
    public void SetWeaponSelectionText(bool value)
    {
        pickUpWeaponText.SetActive(value);
    }
}