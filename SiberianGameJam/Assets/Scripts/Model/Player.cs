using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _distanceForStay;
    [SerializeField] protected bool _direction;

    public bool _ControllerIsBlocked;
    private Vector3 _currentMousePosition;
    private System.Random _random;
    protected Rigidbody2D _rigidbody { get; set; }
    public PlayerState PlayerState { get; set; }
    private GameState _gameState;
    public int _maxHP = 100;
    public int _currentHP;
    public int _baseDamage = 10;

    void Start()
    {
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
                    _currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 90));

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
                DontControlled();
                break;
            case GameState.PlayerAttack:
                DontControlled();
                break;
            case GameState.Wait:
                DontControlled();
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
                DontControlled();
                break;
            case GameState.PlayerAttack:
                DontControlled();
                break;
            case GameState.Wait:
                DontControlled();
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

    public int CountDamage(float _probality)
    {
        if (_probality > _random.Next(0, 100))
        {
            return _baseDamage + _random.Next(0, 10);
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

    public void SetGamesState(GameState gameState)
    {
        _gameState = gameState;
    }
}
