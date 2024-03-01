using System.Collections;
using UnityEngine;

public class ChoppingTentaclesManager : MonoBehaviour
{
    [Header("Chopping Tentacles Set Up")]
    [SerializeField] private Transform[] tentacles;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string choppingSFX;

    /// <summary>
    /// Coroutine responsible for handling the chopping tentacles' behavior.
    /// Activates tentacles one by one with delays between activations, then deactivates them all.
    /// </summary>
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
        DeactivateAllTentacles();
        Debug.Log("Attack 1 Finish");
    }

    /// <summary>
    /// Activates a tentacle at the specified index, starts a coroutine to handle collider activation,
    /// and plays the chopping sound effect.
    /// </summary>
    /// <param name="index">Index of the tentacle to activate.</param>
    private void SlamTentacle(int index)
    {
        tentacles[index].gameObject.SetActive(true);
        StartCoroutine(TentacleWaitCollider(index));
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(choppingSFX);
        }
    }

    /// <summary>
    /// Coroutine responsible for delaying collider activation for a tentacle.
    /// </summary>
    /// <param name="index">Index of the tentacle to activate the collider for.</param>
    /// <returns>Yield instruction to wait for a specific duration.</returns>
    private IEnumerator TentacleWaitCollider(int index)
    {
        yield return new WaitForSeconds(bossData.attack1ColliderActivationDelay);
        TentacleColliderState(index, true);
    }

    /// Sets the collider state for the tentacle at the specified index.
    /// </summary>
    /// <param name="index">Index of the tentacle to modify the collider for.</param>
    /// <param name="state">Desired state of the collider (enabled or disabled).</param>
    private void TentacleColliderState(int index, bool state)
    {
        Collider tentacleCollider = tentacles[index].GetComponent<Collider>();
        if (tentacleCollider != null)
        {
            tentacleCollider.enabled = state;
        }
    }

    /// <summary>
    /// Deactivates all tentacles by disabling their colliders and setting their GameObjects inactive.
    /// </summary>
    private void DeactivateAllTentacles()
    {
        for (int i = 0; i < tentacles.Length; i++)
        {
            TentacleColliderState(i, false);
            tentacles[i].gameObject.SetActive(false);
        }
    }
}