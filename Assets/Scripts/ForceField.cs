using UnityEngine;

public class ForceField : MonoBehaviour
{
    public GameObject forceFieldPrefab;
    public float activationDuration = 5f;
    public float cooldownDuration = 10f;
    public float repelForce = 10f;

    private bool isActivated = false;
    private float activationTimer = 0f;
    private float cooldownTimer = 0f;
    private GameObject currentForceField;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateForceField();
        }

        if (isActivated)
        {
            activationTimer -= Time.deltaTime;
            if (activationTimer <= 0f)
            {
                DeactivateForceField();
            }
        }
        else if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated && (other.gameObject.layer == LayerMask.NameToLayer("Bullet") || other.gameObject.layer == LayerMask.NameToLayer("Enemy")))
        {
            Vector3 repelDirection = (other.transform.position - transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(repelDirection * repelForce, ForceMode.Impulse);
        }
    }

    public void ActivateForceField()
    {
        if (!isActivated && cooldownTimer <= 0f)
        {
            isActivated = true;
            activationTimer = activationDuration;
            currentForceField = Instantiate(forceFieldPrefab, transform.position, transform.rotation, transform);
        }
    }

    private void DeactivateForceField()
    {
        isActivated = false;
        Destroy(currentForceField);
        cooldownTimer = cooldownDuration;
    }
}