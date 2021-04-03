using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private GameController _gameController;

    private List<GameObject> _enemies;
    private Enemy _enemy;
    private Player _player;
    private GameState _gameState;
    private float _timeForAttack = 0.5f;
    private float _currentTime = 0f;

    public int _damageOnHead;
    public int _damageOnBody;
    public int _damageOnArm;
    public bool InFightZone;
    public Enemy Enemy { get { return _enemy; } }

    private void Start()
    {
        _gameState = GameState.Moving;
        _player = FindObjectOfType<Player>();
        _enemies = new List<GameObject>();
    }

    private void Update()
    {
        switch (_gameState)
        {
            case GameState.Moving:
                break;
            case GameState.Wait:
                break;
            case GameState.EnemyAttack:
                _currentTime += Time.deltaTime;
                if (_currentTime > _timeForAttack)
                {
                    _currentTime = 0;
                    _gameController.SetGamesState(GameState.Wait);
                }
                break;
            case GameState.PlayerAttack:
                _currentTime += Time.deltaTime;

                if (_currentTime > _timeForAttack)
                {
                    _currentTime = 0;
                    _gameController.SetGamesState(GameState.EnemyAttack);
                }

                break;
        }
    }

    public void SetGamesState(GameState gameState)
    {
        _gameState = gameState;
        if (_enemy != null)
        {
            _enemy.SetGamesState(GameState.Moving);
        }
    }

    public void InitializeEnemies( ref List<GameObject> enemies)
    {
        _enemies = enemies;

        foreach (GameObject enemy in _enemies)
        {
            EventTrigger eventTrigger = enemy.AddComponent<EventTrigger>();

            EventTrigger.Entry up = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };

            up.callback.AddListener(delegate { ClickOnEnemy(); });
            eventTrigger.triggers.Add(up);
        }

        _ui.GetEnemyUI().GetShark().GetArm().onClick.AddListener(delegate { HitOnArm(); });
        _ui.GetEnemyUI().GetShark().Getbody().onClick.AddListener(delegate { HitOnBody(); }); 
        _ui.GetEnemyUI().GetShark().GetHead().onClick.AddListener(delegate { HitOnHead(); });
    }

    public void ClickOnEnemy()
    {
        if (InFightZone)
        {
            if (_gameState == GameState.Wait || _gameState == GameState.Moving)
            {
                _ui.OpenEnemyUI(_enemy.EnemyType);
                Time.timeScale = 0;
            }
        }
    }

    private void HitOnHead()
    {
        _gameController.SetGamesState(GameState.PlayerAttack);
        _currentTime = 0;

        switch (_enemy.EnemyType)
        {
            case EnemyType.Shark:
                int damage =_player.CountDamage(_enemy._probabilityOnHead);

                if (damage != 0)
                {
                    damage = damage * _damageOnHead;
                    _enemy.SetDamage(damage);
                    _ui.Text(damage.ToString(), _enemy.transform.position);
                }
                else
                {
                    _ui.Text("MISS", _enemy.transform.position);
                }
                
                break;
        }

        _ui.CloseEnemyUI();
        Time.timeScale = 1;
    }

    private void HitOnArm()
    {
        _gameController.SetGamesState(GameState.PlayerAttack);
        _currentTime = 0;

        switch (_enemy.EnemyType)
        {
            case EnemyType.Shark:
                int damage = _player.CountDamage(_enemy._probabilityOnArm);

                if (damage != 0)
                {
                    damage = damage * _damageOnArm;
                    _enemy.SetDamage(damage);
                    _ui.Text(damage.ToString(), _enemy.transform.position);
                }
                else
                {
                    _ui.Text("MISS", _enemy.transform.position);
                }

                break;
        }

        _ui.CloseEnemyUI();
        Time.timeScale = 1;
    }

    private void HitOnBody()
    {
        _gameController.SetGamesState(GameState.PlayerAttack);
        _currentTime = 0;

        switch (_enemy.EnemyType)
        {
            case EnemyType.Shark:
                int damage = _player.CountDamage(_enemy._probabilityOnBody);

                if (damage != 0)
                {
                    damage = damage * _damageOnBody;
                    _enemy.SetDamage(damage);
                    _ui.Text(damage.ToString(), _enemy.transform.position);
                }
                else
                {
                    _ui.Text("MISS", _enemy.transform.position);
                }

                break;
        }

        _ui.CloseEnemyUI();
        Time.timeScale = 1;
    }

    public void InZone()
    {
        InFightZone = true;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;

    }

    public void ClearEnemy()
    {
        _enemy = null;
    }
}
