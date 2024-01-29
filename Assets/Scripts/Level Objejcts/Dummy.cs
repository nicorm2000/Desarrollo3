using UnityEngine;

public class Dummy : MonoBehaviour
{
    [Header("Dummy Set Up")]
    [SerializeField] private float animationDuration;
    [SerializeField] private LayerMask layerToInteract;
    [SerializeField] private string animationTriggerParameter;

    [Header("Hit Marker Dependencies")]
    [SerializeField] private HitMarker hitMarker;


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            hitMarker.HitEnemy();
            animator.SetBool(animationTriggerParameter, true);
            Invoke("ResetAnimationParameter", animationDuration);
        }
    }

    void ResetAnimationParameter()
    {
        animator.SetBool(animationTriggerParameter, false);
    }
}