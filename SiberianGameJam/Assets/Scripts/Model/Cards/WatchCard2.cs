using UnityEngine;


public class WatchCard2 : BaseCard
{
    public WatchCard2(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Watch2;
        _description = "Шанс +6% \nУрон -8";
    }

    public override void UseCard()
    {
        base.UseCard();
    }
}
