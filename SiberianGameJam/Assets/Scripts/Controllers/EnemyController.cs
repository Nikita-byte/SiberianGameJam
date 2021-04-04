using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private GameController _gameController;
    [SerializeField] private CardController _cardController;

    public AudioSource audioSource;
    private List<GameObject> _enemies;
    private Enemy _enemy;
    private Player _player;
    private GameState _gameState;
    private float _timeForAttack = 1f;
    private float _currentTime = 0f;
   
    public bool _isHit;
    public int _damageOnHead;
    public int _damageOnBody;
    public int _damageOnArm;
    public bool InFightZone;
    public Enemy Enemy { get { return _enemy; } }
    private int _number;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _isHit = false;
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

                //if (_isHit)
                //{
                //    _currentTime += Time.deltaTime;
                //    if (_currentTime > _timeForAttack)
                //    {
                //        _currentTime = 0;

                //        _isHit = false;
                //    }
                //}

                break;
            case GameState.PlayerAttack:

                if (_isHit)
                {
                    _currentTime += Time.deltaTime;
                    if (_currentTime > _timeForAttack)
                    {
                        _currentTime = 0;
                        _gameController.SetGamesState(GameState.EnemyAttack);
                        _isHit = false;
                    }
                }

                break;
            case GameState.Dead:
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

        _ui.GetEnemyUI().GetShark().GetArm().onClick.AddListener(delegate { ClickOnButton(0); });
        _ui.GetEnemyUI().GetShark().Getbody().onClick.AddListener(delegate { ClickOnButton(1); }); 
        _ui.GetEnemyUI().GetShark().GetHead().onClick.AddListener(delegate { ClickOnButton(2); });
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

    public void ClickOnButton(int i)
    {
        _ui.CloseEnemyUI();
        _isHit = true;
        _currentTime = 0;
        _number = i;
        Time.timeScale = 1;
        _player.animator.SetTrigger("IsHit");
    }

    public void Hit()
    {
        switch (_number)
        {
            case 0:
                HitOnArm();
                break;
            case 1:
                HitOnBody();
                break;
            case 2:
                HitOnHead();
                break;
        }

        _gameController.ShakeCamera();
        audioSource.Play();
        //_gameController.SetGamesState(GameState.PlayerAttack);
        //_isHit = true;
        //_ui.CloseEnemyUI();
        //Time.timeScale = 1;
    }

    public void HitPlayer()
    {
        var dam = _enemy.GetDamage();
        _gameController.ShakeCamera();
        audioSource.Play();

        if (dam != 0)
        {
            _player.SetDamage(dam);
            _ui.Text("-" + dam.ToString(), _player.transform.position, false);
        }
        else
        {
            _ui.Text("MISS", _player.transform.position, false);
        }
    }

    private void HitOnHead()
    {

        switch (_enemy.EnemyType)
        {
            case EnemyType.Shark:

                int damage = 0;

                if (_cardController.ActiveCard != null)
                {

                    switch (_cardController.ActiveCard.CardType)
                    {
                        case CardType.HP:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, 0, 0);
                            break;
                        case CardType.HP2:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, 0, 0);
                            break;
                        //case CardType.King:
                        //    break;
                        case CardType.Octosmash:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, -0.5f, 5);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Octosmash2:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, -1f, 10);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Octosmash3:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, -1.5f, 15);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Random:
                            BaseCard temp = _cardController.CreateRandomCard();

                            if (temp != null)
                            {
                                switch (temp.CardType)
                                {
                                    case CardType.HP:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, 0, 0);
                                        temp.UseCard();
                                        break;
                                    case CardType.HP2:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, 0, 0);
                                        temp.UseCard();
                                        break;
                                    //case CardType.King:
                                    //    break;
                                    case CardType.Octosmash:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, -0.5f, 5);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Octosmash2:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, -1f, 10);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Octosmash3:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, -1.5f, 15);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Watch:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, 3f, -4);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Watch2:
                                        damage = _player.CountDamage(_enemy._probabilityOnHead, 6, -8);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                        //case CardType.Aquaman:
                                        //    break;
                                }
                            }

                            break;

                        case CardType.Watch:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, 3f, -4);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Watch2:
                            damage = _player.CountDamage(_enemy._probabilityOnHead, 6, -8);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                            //case CardType.Aquaman:
                            //    break;
                    }
                }
                else
                {
                    damage = _player.CountDamage(_enemy._probabilityOnHead, 0, 0);
                }

                if (damage != 0)
                {
                    damage = damage * _damageOnHead;
                    _enemy.SetDamage(damage);
                    _ui.Text("-" + damage.ToString(), _enemy.transform.position, false);
                }
                else
                {
                    _ui.Text("MISS", _enemy.transform.position, false);
                }
                
                break;
        }
    }

    private void HitOnArm()
    {
        _gameController.SetGamesState(GameState.PlayerAttack);
        _currentTime = 0;

        switch (_enemy.EnemyType)
        {
            case EnemyType.Shark:

                int damage = 0;

                if (_cardController.ActiveCard != null)
                {

                    switch (_cardController.ActiveCard.CardType)
                    {
                        case CardType.HP:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, 0, 0);
                            break;
                        case CardType.HP2:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, 0, 0);
                            break;
                        //case CardType.King:
                        //    break;
                        case CardType.Octosmash:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, -0.5f, 5);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Octosmash2:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, -1f, 10);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Octosmash3:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, -1.5f, 15);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Random:
                            BaseCard temp = _cardController.CreateRandomCard();

                            if (temp != null)
                            {
                                switch (temp.CardType)
                                {
                                    case CardType.HP:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, 0, 0);
                                        temp.UseCard();
                                        break;
                                    case CardType.HP2:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, 0, 0);
                                        temp.UseCard();
                                        break;
                                    //case CardType.King:
                                    //    break;
                                    case CardType.Octosmash:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, -0.5f, 5);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Octosmash2:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, -1f, 10);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Octosmash3:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, -1.5f, 15);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Watch:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, 3f, -4);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Watch2:
                                        damage = _player.CountDamage(_enemy._probabilityOnArm, 6, -8);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                        //case CardType.Aquaman:
                                        //    break;
                                }
                            }
                            break;
                        case CardType.Watch:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, 3f, -4);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Watch2:
                            damage = _player.CountDamage(_enemy._probabilityOnArm, 6, -8);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                            //case CardType.Aquaman:
                            //    break;
                    }
                }
                else
                {
                    damage = _player.CountDamage(_enemy._probabilityOnArm, 0, 0);
                }

                if (damage != 0)
                {
                    damage = damage * _damageOnArm;
                    _enemy.SetDamage(damage);
                    _ui.Text("-" + damage.ToString(), _enemy.transform.position, false);
                }
                else
                {
                    _ui.Text("MISS", _enemy.transform.position, false);
                }

                break;
        }
    }

    private void HitOnBody()
    {
        _gameController.SetGamesState(GameState.PlayerAttack);
        _currentTime = 0;

        switch (_enemy.EnemyType)
        {
            case EnemyType.Shark:

                int damage = 0;

                if (_cardController.ActiveCard != null)
                {

                    switch (_cardController.ActiveCard.CardType)
                    {
                        case CardType.HP:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, 0, 0);
                            break;
                        case CardType.HP2:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, 0, 0);
                            break;
                        //case CardType.King:
                        //    break;
                        case CardType.Octosmash:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, -0.5f, 5);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Octosmash2:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, -1f, 10);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Octosmash3:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, -1.5f, 15);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Random:
                            BaseCard temp = _cardController.CreateRandomCard();

                            if (temp != null)
                            {
                                switch (temp.CardType)
                                {
                                    case CardType.HP:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, 0, 0);
                                        temp.UseCard();
                                        break;
                                    case CardType.HP2:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, 0, 0);
                                        temp.UseCard();
                                        break;
                                    //case CardType.King:
                                    //    break;
                                    case CardType.Octosmash:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, -0.5f, 5);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Octosmash2:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, -1f, 10);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Octosmash3:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, -1.5f, 15);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Watch:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, 3f, -4);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                    case CardType.Watch2:
                                        damage = _player.CountDamage(_enemy._probabilityOnBody, 6, -8);
                                        _cardController.RemoveActiveCArd();
                                        temp.UseCard();
                                        _ui.CloseActiveCard();
                                        break;
                                        //case CardType.Aquaman:
                                        //    break;
                                }
                            }
                            break;
                        case CardType.Watch:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, 3f, -4);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                        case CardType.Watch2:
                            damage = _player.CountDamage(_enemy._probabilityOnBody, 6, -8);
                            _cardController.RemoveActiveCArd();
                            _ui.CloseActiveCard();
                            break;
                            //case CardType.Aquaman:
                            //    break;
                    }
                }
                else
                {
                    damage = _player.CountDamage(_enemy._probabilityOnBody, 0, 0);
                }

                if (damage != 0)
                {
                    damage = damage * _damageOnBody;
                    _enemy.SetDamage(damage);
                    _ui.Text("-" + damage.ToString(), _enemy.transform.position, false);
                }
                else
                {
                    _ui.Text("MISS", _enemy.transform.position, false);
                }

                break;

                //int damage = _player.CountDamage(_enemy._probabilityOnBody);

                //if (damage != 0)
                //{
                //    damage = damage * _damageOnBody;
                //    _enemy.SetDamage(damage);
                //    _ui.Text("-" + damage.ToString(), _enemy.transform.position, false);
                //}
                //else
                //{
                //    _ui.Text("MISS", _enemy.transform.position, false);
                //}

                break;
        }
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
