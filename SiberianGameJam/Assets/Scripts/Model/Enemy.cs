using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected bool _direction;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private Sprite _sprite;

    public int MaxHP;
    public int HP;
    public int damage;
    public float probabilityOfHit;
    private EnemyController _enemyController;
    private GameController _gameController;
    private UI _ui;
    private float _distanceForFight = 30f;
    private float _distanceForHood = 50f;
    private Player _player;
    private bool _isActive;
    private bool _pointerIn;
    private GameState _gameState;
    private System.Random _random;
    public float _probabilityOnHead;
    public float _probabilityOnBody;
    public float _probabilityOnArm;
    public EnemyType EnemyType { get { return _enemyType; } }
    public bool Direction { get { return _direction; } }
    public Animator animator;
    public AudioSource audioSource;

    private void Awake()
    {
        _random = new System.Random();
        audioSource = GetComponent<AudioSource>();
        _gameController = FindObjectOfType<GameController>();
        _ui = FindObjectOfType<UI>();
        _gameState = GameState.Moving;
        _player = FindObjectOfType<Player>();
        _enemyController = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        HP = MaxHP;
        animator = GetComponent<Animator>();
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

            if (CheckDistanceForHood())
            {
                _ui.TurnOnHoodPanel(_direction, HP.ToString(), MaxHP.ToString(), _player._currentHP.ToString(), _player._maxHP.ToString(), _sprite, _player.GetSptite);
            }
            else
            {
                _ui.TurnOffHoodPanel();
            }

            if (CheckDistance())
            {
                if (!_enemyController.InFightZone)
                {
                    _enemyController.InZone();
                }
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
                case GameState.Dead:
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

    public void HitPlayer()
    {
        _enemyController.HitPlayer();
    }

    public void ActivateEnemy(bool isActive)
    {
        _isActive = isActive;
    }

    public bool CheckDistance()
    {
        return Math.Abs(_player.transform.position.x - transform.position.x) > _distanceForFight ? false : true;
    }

    public bool CheckDistanceForHood()
    {
        return Math.Abs(_player.transform.position.x - transform.position.x) > _distanceForHood ? false : true;
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
            audioSource.Play();
            _gameController.SetGamesState(GameState.Moving);
            //_player.PlayerState = PlayerState.Move;
            ObjectPool.Pool.ReturnEnemyInPool(gameObject);
        }
    }

    public int GetDamage()
    {
        if (probabilityOfHit > _random.Next(0, 100))
        {
            return damage + _random.Next(0, 10);
        }
        else
        {
            return 0;
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

    public void EndHit()
    {
        _gameController.SetGamesState(GameState.Wait);
        animator.SetTrigger("Hit");
    }
}
