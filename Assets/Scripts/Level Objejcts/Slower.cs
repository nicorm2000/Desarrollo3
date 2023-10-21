using UnityEngine;

public class Slower : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f; // Speed multiplier when inside the slippery zone

    private AIChase aiChase;
    private AIShooterChase aiShooterChase;

    private void OnTriggerEnter(Collider other)
    {
        aiChase = other.GetComponent<AIChase>();
        aiShooterChase = other.GetComponent<AIShooterChase>();

        if (aiChase != null)
        {
            Debug.Log(aiChase.chaseSpeed + " 1");
            aiChase.chaseSpeed /= speedMultiplier;
            Debug.Log(aiChase.chaseSpeed + " 2");
        }
        else if (aiShooterChase != null)
        {
            aiShooterChase.chaseSpeed /= speedMultiplier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        aiChase = other.GetComponent<AIChase>();
        aiShooterChase = other.GetComponent<AIShooterChase>();

        if (aiChase != null)
        {
            Debug.Log(aiChase.chaseSpeed + " 3");
            aiChase.chaseSpeed *= speedMultiplier;
            Debug.Log(aiChase.chaseSpeed + " 4");
        }
        else if (aiShooterChase != null)
        {
            aiShooterChase.chaseSpeed *= speedMultiplier;
        }
    }
}