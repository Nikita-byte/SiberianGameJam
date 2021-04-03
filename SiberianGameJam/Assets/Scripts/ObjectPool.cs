using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public sealed class ObjectPool
{
    private static readonly ObjectPool _instance = new ObjectPool();

    private string OBJECTPOOL = "[Pool]";
    private string ENEMIES = "[Enemies]";
    private string FLOORS = "[Floors]";
    private List<GameObject> _floors;
    private List<GameObject> _enemies;
    private int _floorsCountInPool = 10;
    private int _enemiesCountInPool = 10;
    private GameObject _pool;
    private GameObject _floorsPool;
    private GameObject _enemiesPool;
    private System.Random _rand;
    private int _temp;
    private GameObject _tempObject;
    private GameController _gameController;
    private EnemyController _enemyController;

    public static ObjectPool Pool { get { return _instance; } }

    public void Initialize()
    {
        _pool = new GameObject(OBJECTPOOL);
        _floorsPool = new GameObject(FLOORS);
        _enemiesPool = new GameObject(ENEMIES);
        _floorsPool.transform.SetParent(_pool.transform);
        _enemiesPool.transform.SetParent(_pool.transform);
        _floors = new List<GameObject>();
        _enemies = new List<GameObject>();
        _rand = new System.Random();
        _gameController = GameController.FindObjectOfType<GameController>();
        _enemyController = GameObject.FindObjectOfType<EnemyController>();

        var floors = Resources.LoadAll<GameObject>("Floors");

        foreach (GameObject obj in floors)
        {
            for (int i = 0; i < _floorsCountInPool; i++)
            {
                GameObject go = GameObject.Instantiate(obj, _floorsPool.transform);
                go.GetComponent<Floor>().RightStairs.OnStair += _gameController.NextFloor;
                go.GetComponent<Floor>().LeftStairs.OnStair += _gameController.NextFloor;
                go.SetActive(false);
                _floors.Add(go);
            }
        }

        var enemies = Resources.LoadAll<GameObject>("Enemies");

        foreach (GameObject obj in enemies)
        {
            for (int i = 0; i < _enemiesCountInPool; i++)
            {
                GameObject go = GameObject.Instantiate(obj, _enemiesPool.transform);

                go.SetActive(false);
                _enemies.Add(go);
            }
        }

        _enemyController.InitializeEnemies( ref _enemies);
    }

    public GameObject GetFloor()
    {
        _temp = _rand.Next(_floors.Count);
        _tempObject = _floors[_temp];
        _floors.RemoveAt(_temp);
        return _tempObject;
    }

    public GameObject GetEnemy()
    {
        _temp = _rand.Next(_enemies.Count);
        _tempObject = _enemies[_temp];
        _enemies.RemoveAt(_temp);
        return _tempObject;
    }

    public void ReturnFloorInPool(GameObject gameObject)
    {
        gameObject.GetComponent<Floor>().ActivateRoom(false);
        gameObject.SetActive(false);
        _floors.Add(gameObject);
    }

    public void ReturnEnemyInPool(GameObject gameObject)
    {
        gameObject.GetComponent<Enemy>().ActivateEnemy(false);
        gameObject.GetComponent<Enemy>().SetGamesState(GameState.Moving);
        gameObject.transform.SetParent(_enemiesPool.transform);
        gameObject.SetActive(false);
        _enemies.Add(gameObject);
    }
}
