using UnityEngine;


public class Floor : MonoBehaviour
{
    [SerializeField] private Transform _enemyPos;
    [SerializeField] private GameObject _blackPanel;
    private EnemyController _enemyController;
    private GameObject _enemy;
    public Stairs RightStairs;
    public Stairs LeftStairs;

    private void Awake()
    {
        _enemyController = FindObjectOfType<EnemyController>();
    }

    public void ActivateRoom(bool isActive)
    {
        _blackPanel.SetActive(false);

        if (_enemy != null)
        {
            _enemy.GetComponent<Enemy>().ActivateEnemy(true);
            _enemyController.SetEnemy(_enemy.GetComponent<Enemy>());
        }
    }

    public void OffEnemy()
    {
        if (_enemy != null)
        {
            _enemy.GetComponent<Enemy>().ActivateEnemy(false);
            _enemyController.ClearEnemy();
        }
    }

    public void SetStairsSide(bool side)
    {
        if (side)
        {
            LeftStairs.gameObject.SetActive(false);
            RightStairs.gameObject.SetActive(true);
        }
        else
        {
            RightStairs.gameObject.SetActive(false);
            LeftStairs.gameObject.SetActive(true);
        }
    }

    public void SetEnemy()
    {
        _enemy = ObjectPool.Pool.GetEnemy();
        _enemy.transform.position = _enemyPos.position;
        _enemy.transform.SetParent(gameObject.transform);
        _enemy.SetActive(true);
    }

    public GameObject GetEnemy()
    {
        return _enemy;
    }

    public void ReturnInPool()
    {
        if (_enemy != null)
        {
            _enemy.GetComponent<Enemy>().ActivateEnemy(false);
            ObjectPool.Pool.ReturnEnemyInPool(_enemy);
            _enemy = null;
        }

        _blackPanel.SetActive(true);
        ObjectPool.Pool.ReturnFloorInPool(gameObject);
    }
}
