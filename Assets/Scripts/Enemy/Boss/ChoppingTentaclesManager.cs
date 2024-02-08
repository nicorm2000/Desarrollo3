using System.Collections;
using UnityEngine;

public class ChoppingTentaclesManager : MonoBehaviour
{
    [Header("Chopping Tentacles Set Up")]
    [SerializeField] private Transform[] tentacles;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    public IEnumerator ChoppingTentaclesCoroutine()
    {
        float tentacleSpawnDelayAux = bossData.attack1SpawnDelay;
        int activeTentacleIndex = 0;

        while (activeTentacleIndex < tentacles.Length)
        {
            SlamTentacle(activeTentacleIndex);
            yield return new WaitForSeconds(tentacleSpawnDelayAux);

            activeTentacleIndex++;
            tentacleSpawnDelayAux *= bossData.attack1SpawnDelayMultiplier;
            tentacleSpawnDelayAux = Mathf.Max(tentacleSpawnDelayAux, bossData.attack1SpawnMinimumDelay);
        }
        yield return new WaitForSeconds(bossData.attack1Despawn);
        Debug.Log("Attack 1 Finish");
        DeactivateAllTentacles();
    }

    private void SlamTentacle(int index)
    {
        tentacles[index].gameObject.SetActive(true);
    }

    private void DeactivateAllTentacles()
    {
        foreach (Transform tentacle in tentacles)
        {
            tentacle.gameObject.SetActive(false);
        }
    }
}