using UnityEngine;


public class WatchCard : BaseCard
{
    public WatchCard(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Watch;
        _description = "Шанс +3% \nУрон -4";
    }

    public override void UseCard()
    {
        base.UseCard();
    }
}
