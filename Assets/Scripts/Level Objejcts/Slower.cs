using UnityEngine;

public class Slower : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f;

    private AIChase aiChase;
    private AIShooterChase aiShooterChase;

    private void OnTriggerEnter(Collider other)
    {
        aiChase = other.GetComponent<AIChase>();
        aiShooterChase = other.GetComponent<AIShooterChase>();

        if (aiChase != null)
        {
            aiChase.chaseSpeed /= speedMultiplier;
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
            aiChase.chaseSpeed *= speedMultiplier;
        }
        else if (aiShooterChase != null)
        {
            aiShooterChase.chaseSpeed *= speedMultiplier;
        }
    }
}