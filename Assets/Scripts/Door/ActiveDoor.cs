using UnityEngine;

public class ActiveDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject basket;
    [SerializeField] private float delayTime;
    [SerializeField] private WaveCounter roundCounter;

    void Start()
    {
        door.SetActive(false);

        roundCounter = FindObjectOfType<WaveCounter>();
    }

    void Update()
    {
        if (roundCounter.currentRound == roundCounter.maxRounds)
        {
            basket.SetActive(true);
        }
    }

    public void ActiveObject()
    {
        door.SetActive(true);
    }
}