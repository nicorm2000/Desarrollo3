using System.Collections;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [SerializeField] private float activationInterval;
    [SerializeField] private float activationDuration;
    [SerializeField] private Transform[] minePositions;

    private void Start()
    {
        StartCoroutine(ActivateMinesCoroutine());
    }

    private IEnumerator ActivateMinesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationInterval);
            ActivateRandomMine();
        }
    }

    private void ActivateRandomMine()
    {
        int randomIndex = Random.Range(0, minePositions.Length);
        Mine mine = minePositions[randomIndex].GetComponentInChildren<Mine>();
        if (mine != null && !mine.IsActive())
        {
            mine.Activate(activationDuration);
        }
    }
}