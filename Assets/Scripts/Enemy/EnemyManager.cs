using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static GameObject player;
    public static GameObject mainCamera;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
}