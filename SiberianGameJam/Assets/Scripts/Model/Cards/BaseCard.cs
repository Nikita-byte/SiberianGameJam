using UnityEngine;


public class BaseCard 
{
    public string _description;
    public Sprite _sprite;
    public CardType CardType;
    protected GameController _gameController;
    protected EnemyController _enemyController;
    protected CardController _cardController;
    protected UI _ui;

    public BaseCard(Sprite sprite)
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _enemyController = GameObject.FindObjectOfType<EnemyController>();
        _cardController = GameObject.FindObjectOfType<CardController>();
        _ui = GameObject.FindObjectOfType<UI>();
        _sprite = sprite;
    }

    public virtual void UseCard() { }
}
