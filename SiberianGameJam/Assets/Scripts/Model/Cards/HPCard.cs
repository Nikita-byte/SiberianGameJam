using UnityEngine;


public class HPCard : BaseCard
{
    public HPCard(Sprite sprite) : base(sprite)
    {
        CardType = CardType.HP;
        _description = "Увеличивает здоровье + 15";
    }

    public override void UseCard()
    {
        base.UseCard();

        _ui.Text("+15", _gameController.Player.gameObject.transform.position, true);
        _gameController.Player.AddHP(15);
    }
}
