using UnityEngine;

public class AddRounds : MonoBehaviour
{
    [SerializeField] private WaveCounter roundCounter;
    [SerializeField] private Shop shop;

    public WaveData waveData;

    private int targetLayer;

    private void Start()
    {
        targetLayer = LayerMask.NameToLayer("Slower");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] slowerObjects = GameObject.FindObjectsOfType<GameObject>();

            roundCounter.IncreaseRounds(waveData.addRound);

            shop.ActiveShop();

            foreach (GameObject obj in slowerObjects)
            {
                if (obj.layer == targetLayer)
                {
                    Destroy(obj);
                }
            }
        }
    }
}