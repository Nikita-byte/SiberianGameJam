using UnityEngine;
using DG.Tweening;


public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _floorStartPos;
    [SerializeField][Range(0,100)] private int _probabilityOfSpawn;
    [SerializeField] private float _distanceBetweenPlatforms;
    [SerializeField] private float _movePlatformDuration = 1;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _leftSpawnPos;
    [SerializeField] private Transform _rightSpawnPos;
    [SerializeField] private bool _isRightStairs = false;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private UI _ui;

    private Transform[] _floorsPos;
    private int _maxFloorsOnScene = 5;
    private System.Random _rand;
    private GameState _gameState;

    private GameObject[] _activeFloors;

    private void Start()
    {
        _rand = new System.Random();
        ObjectPool.Pool.Initialize();
        _gameState = GameState.Moving;

        _floorsPos = new Transform[_maxFloorsOnScene];
        GameObject temp;

        for (int i = 0; i < _maxFloorsOnScene; i++)
        {
            temp = new GameObject();
            temp.transform.position = _floorStartPos.position;
            _floorsPos[i] = temp.transform;
        }

        for (int i = 1; i < _floorsPos.Length; i++)
        {
            _floorsPos[i].Translate(Vector3.up * _distanceBetweenPlatforms * i);
        }

        CreatePlatforms();
        SetPlatforms();
        _ui.CloseEnemyUI();
        //InvokeRepeating("MovePlatforms", 0.0f, 3f);
    }

    public void NextFloor()
    {
        MovePlatforms();
    }

    public void SetPlatforms()
    {
        for (int i = 0; i < _activeFloors.Length; i++)
        {
            _activeFloors[i].SetActive(true);
            _activeFloors[i].transform.position = _floorsPos[i].position;
        }
    }

    public void SetGamesState(GameState gameState)
    {
        _gameState = gameState;
        _enemyController.SetGamesState(_gameState);
        _player.SetGamesState(_gameState);
    }

    public void CreatePlatforms()
    {
        _activeFloors = new GameObject[_maxFloorsOnScene];

        for (int i = 0; i < _maxFloorsOnScene; i++)
        {
            _activeFloors[i] = ObjectPool.Pool.GetFloor();
            _activeFloors[i].GetComponent<Floor>().SetStairsSide(_isRightStairs);
            SwitchStairsSide();
            SetObjectsOnFloor(i);
        }

        _activeFloors[2].GetComponent<Floor>().ActivateRoom(true);
        //_activeFloors[0].GetComponent<Floor>().OffEnemy();
        //_activeFloors[1].GetComponent<Floor>().OffEnemy();
    }

    public void SetObjectsOnFloor(int number)
    {
        float probability = _rand.Next(100);

        if(probability < _probabilityOfSpawn)
        {
            var tempFloor = _activeFloors[number].GetComponent<Floor>();
            tempFloor.SetEnemy();
        }
    }

    public void MovePlatforms()
    {
        for (int i = 0; i < _maxFloorsOnScene; i++)
        {
            if (i == 0)
            {
                _activeFloors[i].GetComponent<Floor>().ReturnInPool();
            }
            else
            {
                _activeFloors[i].transform.DOMove(_floorsPos[i - 1].position, _movePlatformDuration);
                _activeFloors[i - 1] = _activeFloors[i];
            }
        }

        _activeFloors[2].GetComponent<Floor>().ActivateRoom(true);
        //_activeFloors[0].GetComponent<Floor>().OffEnemy();
        //_activeFloors[1].GetComponent<Floor>().OffEnemy();

        _activeFloors[_maxFloorsOnScene - 1] = ObjectPool.Pool.GetFloor();
        _activeFloors[_maxFloorsOnScene - 1].GetComponent<Floor>().SetStairsSide(_isRightStairs);
        SwitchStairsSide();

        SetObjectsOnFloor(_maxFloorsOnScene - 1);

        _activeFloors[_maxFloorsOnScene - 1].transform.position = _floorsPos[_maxFloorsOnScene - 1].position;
        _activeFloors[_maxFloorsOnScene - 1].SetActive(true);
    }

    private void SwitchStairsSide()
    {
        if (_isRightStairs)
        {
            _isRightStairs = false;
        }
        else
        {
            _isRightStairs = true;
        }
    }
}
