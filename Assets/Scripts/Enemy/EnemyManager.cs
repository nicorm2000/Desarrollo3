using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static GameObject player;
    public static GameObject mainCamera;
    public static int enemyCount;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
}