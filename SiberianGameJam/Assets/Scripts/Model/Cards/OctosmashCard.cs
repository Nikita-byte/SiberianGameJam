using UnityEngine;


public class OctosmashCard : BaseCard
{
    public OctosmashCard(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Octosmash;
        _description = "Шанс -0.5% \nУрон +5";
    }

    public override void UseCard()
    {
        base.UseCard();

        _ui.Text(_description, _gameController.Player.gameObject.transform.position, true);
        _cardController.SetActiveCard(this);
    }
}
