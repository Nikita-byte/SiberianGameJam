using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class CardController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private UI _ui;

    [SerializeField] private CardsUI _first;
    [SerializeField] private CardsUI _second;
    [SerializeField] private CardsUI _third;
    [SerializeField] private CardsUI _fourth;
    [SerializeField] private CardsUI _fifth;

    [SerializeField] int _countHP;
    [SerializeField] int _countHP2;
    [SerializeField] int _countKing;
    [SerializeField] int _countOctosmash;
    [SerializeField] int _countOctosmash2;
    [SerializeField] int _countOctosmash3;
    [SerializeField] int _countRandom;
    [SerializeField] int _countWatch;
    [SerializeField] int _countWatch2;
    [SerializeField] int _countAquaman;
    [SerializeField] int _countMycards;

    private BaseCard _activeCard;
    private Player _player;
    private System.Random _rand;
    private GameState _gameState;
    private bool _isChangeCard;
    private float _time = 1;
    private float _currentTime = 0;

    private bool _firstIsEmpty;
    private bool _secondIsEmpty;
    private bool _thirdIsEmpty;
    private bool _fourthIsEmpty;
    private bool _fifthIsEmpty;


    private List<BaseCard> _cards;
    public List<BaseCard> _Mycards;
    public Dictionary<int, BaseCard> _activeCards;
    public BaseCard ActiveCard { get { return _activeCard; } }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _firstIsEmpty = false;
        _secondIsEmpty = false;
        _thirdIsEmpty = false;
        _fourthIsEmpty = false;
        _fifthIsEmpty = false;
        _activeCard = null;

        _gameState = GameState.Moving;
        _isChangeCard = false;
        _cards = new List<BaseCard>();
        _Mycards = new List<BaseCard>();
        _activeCards = new Dictionary<int, BaseCard>();
        _rand = new System.Random();

        for (int i = 0; i < _countHP; i++)
        {
            _cards.Add(new HPCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.HP]]));
        }

        for (int i = 0; i < _countHP2; i++)
        {
            _cards.Add(new HPCard2(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.HP2]]));
        }

        //for (int i = 0; i < _countKing; i++)
        //{
        //    _cards.Add(new KingCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.King]]));
        //}

        for (int i = 0; i < _countOctosmash; i++)
        {
            _cards.Add(new OctosmashCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Octosmash]]));
        }

        for (int i = 0; i < _countOctosmash2; i++)
        {
            _cards.Add(new OctosmashCard2(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Octosmash2]]));
        }

        for (int i = 0; i < _countOctosmash3; i++)
        {
            _cards.Add(new OctosmashCard3(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Octosmash3]]));
        }

        for (int i = 0; i < _countRandom; i++)
        {
            _cards.Add(new RandomCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Random]]));
        }

        for (int i = 0; i < _countWatch; i++)
        {
            _cards.Add(new WatchCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Watch]]));
        }

        for (int i = 0; i < _countWatch2; i++)
        {
            _cards.Add(new WatchCard2(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Watch2]]));
        }

        //for (int i = 0; i < _countAquaman; i++)
        //{
        //    _cards.Add(new AquamanCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Aquaman]]));
        //}

        for (int i = 0; i < _countMycards; i++)
        {
            var number = _rand.Next(0, _cards.Count);
            var temp = _cards[number];
            _cards.RemoveAt(number);

            _Mycards.Add(temp);
        }

        for (int i = 0; i < 5; i++)
        {
            var number = _rand.Next(0, _Mycards.Count);
            var temp = _Mycards[number];

            _Mycards.RemoveAt(number);

            _activeCards[i] = temp;
        }

        RefreshCardSprite(0);
        RefreshCardSprite(1);
        RefreshCardSprite(2);
        RefreshCardSprite(3);
        RefreshCardSprite(4);

        _ui.CardPanel.First.onClick.AddListener(delegate { UseCard(0); });
        _ui.CardPanel.Second.onClick.AddListener(delegate { UseCard(1); });
        _ui.CardPanel.Third.onClick.AddListener(delegate { UseCard(2); });
        _ui.CardPanel.Fourth.onClick.AddListener(delegate { UseCard(3); });
        _ui.CardPanel.Fifth.onClick.AddListener(delegate { UseCard(4); });
    }

    public void RefreshCardSprite(int i)
    {
        switch (i)
        {
            case 0:
                _first.GetComponent<Image>().sprite = _activeCards[0]._sprite;
                _first._discription = _activeCards[0]._description;
                break;
            case 1:
                _second.GetComponent<Image>().sprite = _activeCards[1]._sprite;
                _second._discription = _activeCards[1]._description;
                break;
            case 2:
                _third.GetComponent<Image>().sprite = _activeCards[2]._sprite;
                _third._discription = _activeCards[2]._description;
                break;
            case 3:
                _fourth.GetComponent<Image>().sprite = _activeCards[3]._sprite;
                _fourth._discription = _activeCards[3]._description;
                break;
            case 4:
                _fifth.GetComponent<Image>().sprite = _activeCards[4]._sprite;
                _fifth._discription = _activeCards[4]._description;
                break;
        }
    }

    private void Update()
    {
        if(_isChangeCard)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _time)
            {
                _isChangeCard = false;
                _currentTime = 0;

                if (_firstIsEmpty)
                {
                    if (_Mycards.Count != 0)
                    {
                        var number = _rand.Next(0, _Mycards.Count);
                        var temp = _Mycards[number];

                        _Mycards.RemoveAt(number);

                        _activeCards[0] = temp;
                        _first.RefreshCard();
                        RefreshCardSprite(0);
                        _firstIsEmpty = false;
                    }
                }

                if (_secondIsEmpty)
                {
                    if (_Mycards.Count != 0)
                    {
                        var number = _rand.Next(0, _Mycards.Count);
                        var temp = _Mycards[number];

                        _Mycards.RemoveAt(number);

                        _activeCards[1] = temp;
                        _second.RefreshCard();
                        RefreshCardSprite(1);
                        _secondIsEmpty = false;
                    }
                }

                if (_thirdIsEmpty)
                {
                    if (_Mycards.Count != 0)
                    {
                        var number = _rand.Next(0, _Mycards.Count);
                        var temp = _Mycards[number];

                        _Mycards.RemoveAt(number);

                        _activeCards[2] = temp;
                        _third.RefreshCard();
                        RefreshCardSprite(2);
                        _thirdIsEmpty = false;
                    }
                }

                if (_fourthIsEmpty)
                {
                    if (_Mycards.Count != 0)
                    {
                        var number = _rand.Next(0, _Mycards.Count);
                        var temp = _Mycards[number];

                        _Mycards.RemoveAt(number);

                        _activeCards[3] = temp;
                        _fourth.RefreshCard();
                        RefreshCardSprite(3);
                        _fourthIsEmpty = false;
                    }
                }

                if (_fifthIsEmpty)
                {
                    if (_Mycards.Count != 0)
                    {
                        var number = _rand.Next(0, _Mycards.Count);
                        var temp = _Mycards[number];

                        _Mycards.RemoveAt(number);

                        _activeCards[4] = temp;
                        _fifth.RefreshCard();
                        RefreshCardSprite(4);
                        _fifthIsEmpty = false;
                    }
                }
            }
        }
    }

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void UseCard(int i)
    {
        _player.PlayerState = PlayerState.Stay;

        switch (_gameState)
        {
            case GameState.Dead:
                break;
            case GameState.EnemyAttack:
                break;
            case GameState.PlayerAttack:
                break;
            case GameState.Wait:
            case GameState.Moving:

                switch (i)
                {
                    case 0:
                        if (_activeCard == null)
                        {
                            _first.OffCard();
                            _activeCards[0].UseCard();
                            _activeCard = _activeCards[i];
                            _activeCards.Remove(0);
                            _firstIsEmpty = true;
                        }
                        break;
                    case 1:
                        if (_activeCard == null)
                        {
                            _second.OffCard();
                            _activeCards[1].UseCard();
                            _activeCard = _activeCards[i];
                            _activeCards.Remove(1);
                            _secondIsEmpty = true;
                        }
                        break;
                    case 2:
                        if (_activeCard == null)
                        {
                            _third.OffCard();
                            _activeCards[2].UseCard();
                            _activeCard = _activeCards[i];
                            _activeCards.Remove(2);
                            _thirdIsEmpty = true;
                        }
                        break;
                    case 3:
                        if (_activeCard == null)
                        {
                            _fourth.OffCard();
                            _activeCards[3].UseCard(); 
                            _activeCard = _activeCards[i];
                            _activeCards.Remove(3);
                            _fourthIsEmpty = true;
                        }
                        break;
                    case 4:
                        if (_activeCard == null)
                        {
                            _fifth.OffCard();
                            _activeCard = _activeCards[i];
                            _activeCards[4].UseCard();
                            _activeCards.Remove(4);
                            _fifthIsEmpty = true;
                        }
                        break;
                }

                if (_gameState == GameState.Moving)
                {
                    _isChangeCard = true;
                }

                break;
        }
    }

    public void SetActiveCard(BaseCard card)
    {
        _activeCard = card;

        _ui.OpenActiveCard(_enemyController.Enemy.Direction, _activeCard._sprite);
    }

    public void RemoveActiveCArd()
    {
        _activeCard = null;
    }

    public BaseCard CreateRandomCard()
    {
        switch (_rand.Next(0, 7))
        {
            case 0:
            return new HPCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.HP]]);
                break;
            case 1:
                return new HPCard2(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.HP2]]);
                break;
            case 2:
                return new OctosmashCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Octosmash]]);
                break;
            case 3:
                return new OctosmashCard2(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Octosmash2]]);
                break;
            case 4:
                return new OctosmashCard3(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Octosmash3]]);
                break;
            case 5:
                return new WatchCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Watch]]);
                break;
            case 6:
                return new WatchCard2(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Watch2]]);
                break;
            //case 5:
            //    return new RandomCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Random]]);
            //    break;
                //case 9:
                //    return new AquamanCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.Aquaman]]);
                //    break;
                //case 2:
                //    return new KingCard(ObjectPool.Pool._cardSprites[CardManager.GameObjects[CardType.King]]);
                //    break;
        }

        return null;
    }
}
