using UnityEngine;


public class RandomCard : BaseCard
{
    public RandomCard(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Random;
        _description = "Никаких гарантий";
    }

    public override void UseCard()
    {
        base.UseCard();
    }
}
