using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private GameObject map;

    public bool isMapActive = false;

    public void ActivateMap()
    {
        isMapActive = true;

        map.SetActive(true);
    }

    public void DeactivateMap()
    {
        isMapActive = false;

        map.SetActive(false);
    }
}