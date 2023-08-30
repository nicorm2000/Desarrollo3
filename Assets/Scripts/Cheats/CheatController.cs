using UnityEngine;

public class CheatController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPlaceholder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DestroyEnemyPlaceholder();
        }
    }

    private void DestroyEnemyPlaceholder()
    {
        if (enemyPlaceholder != null)
        {
            Destroy(enemyPlaceholder);
        }
    }
}