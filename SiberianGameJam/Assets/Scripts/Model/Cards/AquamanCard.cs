using UnityEngine;


public class AquamanCard : BaseCard
{
    public AquamanCard(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Aquaman;
        _description = "Аквамен";
    }

    public override void UseCard()
    {
        base.UseCard();
    }
}
