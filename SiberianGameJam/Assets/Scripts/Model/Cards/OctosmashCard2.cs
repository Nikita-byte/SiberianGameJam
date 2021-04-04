using UnityEngine;


public class OctosmashCard2 : BaseCard
{
    public OctosmashCard2(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Octosmash2;
        _description = "Шанс -1% \nУрон +10";
    }

    public override void UseCard()
    {
        base.UseCard();

        _ui.Text(_description, _gameController.Player.gameObject.transform.position, true);
        _cardController.SetActiveCard(this);
    }
}
