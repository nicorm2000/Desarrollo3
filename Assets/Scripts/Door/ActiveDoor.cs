using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject basket;
    [SerializeField] private float delayTime;
    [SerializeField] private RoundCounter roundCounter;

    private bool isActive = false;

    void Start()
    {
        door.SetActive(false);
        isActive = false;

        roundCounter = FindObjectOfType<RoundCounter>();
    }

    void Update()
    {
        if (roundCounter.currentRound == roundCounter.maxRounds && IsActive() == false)
        {
            StartCoroutine(PlayAnimationAndActivateObject());
        }

        IEnumerator PlayAnimationAndActivateObject()
        {
            basket.SetActive(true);

            yield return new WaitForSeconds(delayTime);

            ActiveObject();
        }
    }

    public void ActiveObject()
    {
        door.SetActive(true);
        isActive = true;
    }

    public bool IsActive()
    {
        if (isActive == false)
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}
