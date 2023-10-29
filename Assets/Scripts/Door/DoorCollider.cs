using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] private WaveCounter roundCounter;
    [SerializeField] private GameObject spawnWeaponSelect;
    [SerializeField] private GameObject doorCollider;
    [SerializeField] private GameObject basket;

    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        roundCounter = FindObjectOfType<WaveCounter>();
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            player.transform.position = spawnWeaponSelect.transform.position;
            doorCollider.SetActive(false);
            basket.SetActive(false);

            roundCounter.maxRounds += 5;
        }
    }
}