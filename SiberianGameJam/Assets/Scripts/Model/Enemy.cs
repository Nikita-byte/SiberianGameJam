using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected bool _direction;
    [SerializeField] private EnemyType _enemyType;
    public int MaxHP;
    public int HP;
    private EnemyController _enemyController;
    private GameController _gameController;
    private UI _ui;
    private float _distanceForFight = 40f;
    private Player _player;
    private bool _isActive;
    private bool _pointerIn;
    private GameState _gameState;
    public float _probabilityOnHead;
    public float _probabilityOnBody;
    public float _probabilityOnArm;
    public EnemyType EnemyType { get { return _enemyType; } }

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _ui = FindObjectOfType<UI>();
        _gameState = GameState.Moving;
        _player = FindObjectOfType<Player>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        HP = MaxHP;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_player.transform.position.x > transform.position.x)
            {
                if (_direction == true)
                {
                    Turn();
                }
            }
            else
            {
                if (_direction == false)
                {
                    Turn();
                }
            }

            if (CheckDistance())
            {
                if (!_enemyController.InFightZone)
                {
                    _enemyController.InZone();
                }

                //if (CheckDistanceForFight())
                //{
                //    if (_gameState == GameState.MovingToFight)
                //    {
                //        _gameController.SetGamesState(GameState.PlayerAttack);
                //        _gameState = GameState.PlayerAttack;
                //        _ui.OpenEnemyUI();
                //    }
                //    else
                //    {
                //        _gameController.SetGamesState(GameState.EnemyAttack);
                //        _gameState = GameState.EnemyAttack;
                //    }
                //}
            }
            else
            {
                _enemyController.InFightZone = false;
            }

            switch (_gameState)
            {
                case GameState.Moving:                 
                    break;
                case GameState.EnemyAttack:
                    break;
                case GameState.PlayerAttack:
                    break;
                case GameState.Wait:
                    break;
            }
        }
    }

    public void Turn()
    {
        transform.rotation *= Quaternion.Euler(0, 180, 0);

        if (_direction)
        {
            _direction = false;
        }
        else
        {
            _direction = true;
        }

    }

    public void ActivateEnemy(bool isActive)
    {
        _isActive = isActive;
    }

    public bool CheckDistance()
    {
        return Math.Abs(_player.transform.position.x - transform.position.x) > _distanceForFight ? false : true;
    }

    public void CheckDirection()
    {
        if (_player.transform.position.x > transform.position.x)
        {
            if (_direction == true)
            {
                Turn();
            }
        }
        else
        {
            if (_direction == false)
            {
                Turn();
            }
        }
    }

    public void SetDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            _gameController.SetGamesState(GameState.Moving);
            ObjectPool.Pool.ReturnEnemyInPool(gameObject);
        }
    }

    public void SetGamesState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerIn = false;
    }
}
