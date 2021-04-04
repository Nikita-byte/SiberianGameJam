using UnityEngine;


public class KingCard : BaseCard
{
    public KingCard(Sprite sprite) : base(sprite)
    {
        CardType = CardType.King;
        _description = "Карта ИМБА - НЕ ТРОГАЙ";
    }

    public override void UseCard()
    {
        base.UseCard();
    }
}
