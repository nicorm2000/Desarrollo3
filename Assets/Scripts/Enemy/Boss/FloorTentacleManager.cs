using System.Collections;
using UnityEngine;

public class FloorTentacleManager : MonoBehaviour
{
    [Header("Mines Set Up")]
    [SerializeField] private Transform[] minePositions;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    public IEnumerator ActivateFloorTentacleCoroutine(int tentaclesPerIteration)
    {
        int iteration = 0;
        while (iteration < bossData.attack3AmountOfIterations)
        {
            yield return new WaitForSeconds(bossData.attack3ActivationInterval);
            ActivateRandomFloorTentacle(tentaclesPerIteration);
            Debug.Log(iteration);
            iteration++;
        }
    }

    private void ActivateRandomFloorTentacle(int tentaclesPerIteration)
    {
        int tentaclesActivatedThisIteration = 0;

        while (tentaclesActivatedThisIteration < tentaclesPerIteration)
        {
            int randomIndex = Random.Range(0, minePositions.Length);
            FloorTentacle floorTentacle = minePositions[randomIndex].GetComponentInChildren<FloorTentacle>();

            if (floorTentacle != null && !floorTentacle.IsFloorTentacleActive())
            {
                floorTentacle.Activate(bossData.attack3ActivationDuration);
                tentaclesActivatedThisIteration++;
            }
        }
    }
}