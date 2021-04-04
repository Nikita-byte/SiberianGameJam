using UnityEngine;

public class OctosmashCard3 : BaseCard
{
    public OctosmashCard3(Sprite sprite) : base(sprite)
    {
        CardType = CardType.Octosmash3;
        _description = "Шанс -1.5% \nУрон +15";
    }

    public override void UseCard()
    {
        base.UseCard();

        _ui.Text(_description, _gameController.Player.gameObject.transform.position, true);
        _cardController.SetActiveCard(this);
    }
}
