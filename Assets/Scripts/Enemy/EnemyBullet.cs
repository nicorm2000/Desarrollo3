using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float damage;

    private float timer;

    public EnemyData enemyData;
    public PlayerData playerData;

    public GameObject player;

    private void Start()
    {
        damage = enemyData.damage;
        timer = enemyData.lifeSpawn;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * enemyData.bulletSpeed);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    Debug.Log("Damage!");
        //    collision.GetComponent<HealthSystem>().TakeDamage(damage);
        //    Destroy(gameObject);
        //}

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        //{
        //    Destroy(gameObject);
        //}
    }
}
