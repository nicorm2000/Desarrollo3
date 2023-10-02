using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string suhsiName;
    public GameObject model;
    public bool isMelee;
    public GameObject bullet;
    public float damage;
    public float movementSpeed;
    public Animator animator;
    public string spawnAnimationName;
    public float spawnAnimationDuration;
    public string deathAnimationName;
    public float deathAnimationDuration;
    public EnemyDropData dropData;
}