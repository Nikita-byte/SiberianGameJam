using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _distanceForStay;
    [SerializeField] bool _direction;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private GameController gameController;
    [SerializeField] UI _uI;

    public bool _ControllerIsBlocked;
    private Vector3 _currentMousePosition;
    private System.Random _random;
    protected Rigidbody2D _rigidbody { get; set; }
    public PlayerState PlayerState { get; set; }
    public Sprite GetSptite { get { return _sprite; } }
    public bool Direction { get { return _direction; } }
    public GameState _gameState;
    public int _maxHP = 100;
    public int _currentHP;
    public int _baseDamage = 10;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        _random = new System.Random();
        _currentHP = _maxHP;
        _gameState = GameState.Moving;
        _rigidbody = GetComponent<Rigidbody2D>();
        PlayerState = PlayerState.Stay;
        _currentMousePosition = transform.position;
    }

    void Update()
    {
        switch (_gameState)
        {
            case GameState.Moving:
                if (Input.GetMouseButtonDown(0))
                {
                    _currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 78));

                    if (CheckDistance())
                    {
                        PlayerState = PlayerState.Move;
                    }
                    else
                    {
                        PlayerState = PlayerState.Stay;
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    PlayerState = PlayerState.Stay;
                }
                break;
            case GameState.EnemyAttack:
                PlayerState = PlayerState.Stay;
                DontControlled();
                break;
            case GameState.PlayerAttack:
                PlayerState = PlayerState.Stay;
                DontControlled();
                break;
            case GameState.Wait:
                PlayerState = PlayerState.Stay;
                DontControlled();
                break;
            case GameState.Dead:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (_gameState)
        {
            case GameState.Moving:
                Move();
                break;
            case GameState.EnemyAttack:
                PlayerState = PlayerState.Stay;
                DontControlled();
                break;
            case GameState.PlayerAttack:
                PlayerState = PlayerState.Stay;
                DontControlled();
                break;
            case GameState.Wait:
                PlayerState = PlayerState.Stay;
                DontControlled();
                break;
            case GameState.Dead:
                break;
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

    public void Move()
    {
        animator.SetFloat("IsMoving", Math.Abs(_rigidbody.velocity.x));

        switch (PlayerState)
        {
            case PlayerState.Stay:
                DontControlled();
                break;
            case PlayerState.Move:
                CheckDirection();
                SetSpeed();
                break;
        }
    }

    public int CountDamage(float _probality, float addPr, int addDamage)
    {
        if (_probality + addPr > _random.Next(0, 100))
        {
            return _baseDamage + _random.Next(0, 10) + addDamage;
        }
        else
        {
            return 0;
        }
    }

    public bool CheckDistance()
    {
        return Math.Abs(_currentMousePosition.x - transform.position.x) < _distanceForStay ? false : true;
    }

    public void DontControlled()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    public void ChangePlayerState(PlayerState playerState)
    {
        PlayerState = playerState;
    }

    public void CheckDirection()
    {
        if (_currentMousePosition.x > transform.position.x)
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

    public void SetSpeed()
    {
        if (CheckDistance())
        {
            var deltaTime = Time.deltaTime;
            if (_direction == false)
            {
                _rigidbody.velocity = new Vector2(_speed * deltaTime, _rigidbody.velocity.y);
            }
            if (_direction == true)
            {
                _rigidbody.velocity = new Vector2(-_speed * deltaTime, _rigidbody.velocity.y);
            }
        }
        else
        {
            PlayerState = PlayerState.Stay;
        }
    }

    public void AddHP(int hp)
    {
        _currentHP += hp;

        if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
    }

    public void SetDamage(int damage)
    {
        _currentHP -= damage;

        if (_currentHP <= 0)
        {
            _uI.deadPanel.gameObject.SetActive(true);
        }
    }

    public void SetGamesState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void Hot()
    {
        enemyController.Hit();
    }

    public void EndHit()
    {
        //gameController.SetGamesState(GameState.EnemyAttack);
        enemyController.Enemy.animator.SetTrigger("Hit");
        animator.SetTrigger("IsHit");
    }
}
