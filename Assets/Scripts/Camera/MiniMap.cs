using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private GameObject map;

    public bool isMapActive = true;

    public void ActivateMap()
    {
        isMapActive = false;

        map.SetActive(true);
    }

    public void DeactivateMap()
    {
        isMapActive = true;

        map.SetActive(false);
    }
}