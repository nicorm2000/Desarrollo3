using UnityEngine;

public class Dummy : MonoBehaviour
{
    [Header("Dummy Set Up")]
    [SerializeField] private float animationDuration;
    [SerializeField] private LayerMask layerToInteract;
    [SerializeField] private string animationTriggerParameter;

    [Header("Hit Marker Dependencies")]
    [SerializeField] private HitMarker hitMarker;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string dummyHit;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (((Constants.ONE << collision.gameObject.layer) & layerToInteract) != Constants.ZERO)
        {
            hitMarker.HitEnemy();
            animator.SetBool(animationTriggerParameter, true);
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(dummyHit);
            }
            Invoke("ResetAnimationParameter", animationDuration);
        }
    }

    void ResetAnimationParameter()
    {
        animator.SetBool(animationTriggerParameter, false);
    }
}