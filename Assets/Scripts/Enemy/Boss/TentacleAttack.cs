using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAttack : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Enemy Data Dependencies")]
    [SerializeField] private BossData bossData;
    [SerializeField] private EnemyData enemyData;

    [Header("Capsule Collider Dependencies")]
    [SerializeField] private BoxCollider boxCollider;
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
        damage = bossData.attack1Damage;
        invulnerability = true;
        StartCoroutine(CooldownCoroutine());
    }

    private void Update()
    {
        if (startAttack == true)
        {
            boxCollider.enabled = false;
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            boxCollider.enabled = true;
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
                    //_audioManager.PlaySound(enemyData.attack);
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
