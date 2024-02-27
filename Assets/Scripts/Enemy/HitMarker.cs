using UnityEngine;

public class HitMarker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemy;

    private float _timer;
    public float _maxTimer;
    private void Start()
    {
        _timer = _maxTimer;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0)
        {
            _timer = _maxTimer;
            ResetEnemyColor();
        }   
    }

    public void HitEnemy() 
    {
        enemy.color = Color.red;
    }

    public void ResetEnemyColor() 
    {
        enemy.color = Color.white;
    }
}