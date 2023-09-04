using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDropData", menuName = "Custom/Enemy Drop Data")]
public class EnemyDropData : ScriptableObject
{
    public List<GameObject> splashSprites;
    public List<Material> materials;
    public float objectLifespan; // Lifespan in seconds
}