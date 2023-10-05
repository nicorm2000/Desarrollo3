using UnityEngine;

public class CheatController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPlaceholder;

    public EnemyData[] enemyData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DestroyEnemyPlaceholder();
        }
    }

    private void DestroyEnemyPlaceholder()
    {
        for (int i = 0; i < enemyData.Length; i++)

        if (enemyData[i].model != null)
        {
            Destroy(enemyData[i].model);
        }
    }
}