using System.Collections;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Enemy Data Dependencies")]
    [SerializeField] private EnemyData enemyData;

    [Header("Capsule Collider Dependencies")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float maxTimeToResetAttack;

    private float timer;
    private bool startAttack;

    private AudioManager _audioManager;
    private float damage;
    private bool invulnerability = false;

    private void Start()
    {
        timer = maxTimeToResetAttack;
        _audioManager = GetComponent<AudioManager>();
        damage = enemyData.damage;
        invulnerability = true;
        StartCoroutine(CooldownCoroutine());
    }

    private void Update()
    {
        if (startAttack == true)
        {
            capsuleCollider.enabled = false;
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            capsuleCollider.enabled = true;
            timer = maxTimeToResetAttack;
            startAttack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO && !playerData.isDashing)
        {
            if (!invulnerability && !playerData._isDead)
            {
                if (!AudioManager.muteSFX)
                {
                    _audioManager.PlaySound(enemyData.attack);
                }

                other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
                startAttack = true;
                invulnerability = true;
            }
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(playerData.invulnerabilityTime);
        invulnerability = false;
    }
}