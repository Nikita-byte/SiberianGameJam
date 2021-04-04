using UnityEngine;


public class HPCard2 : BaseCard
{
    public HPCard2(Sprite sprite) : base(sprite)
    {
        CardType = CardType.HP2;
        _description = "Увеличивает здоровье + 30";
    }

    public override void UseCard()
    {
        base.UseCard();

        _ui.Text("+30", _gameController.Player.gameObject.transform.position, true);
        _gameController.Player.AddHP(30);
    }
}
