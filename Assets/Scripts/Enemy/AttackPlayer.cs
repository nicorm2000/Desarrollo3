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

    private AudioManager _audioManager;
    private float damage;
    private bool invulnerability = false;

    private void Start()
    {
        _audioManager = GetComponent<AudioManager>();
        damage = enemyData.damage;
        invulnerability = true;
        StartCoroutine(CooldownCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO && !playerData.isDashing)
        {
            if (!invulnerability)
            {
                if (!AudioManager.muteSFX)
                {
                    _audioManager.PlaySound(enemyData.attack);
                }
                other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
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