using System.Collections.Generic;


public sealed class CardManager
{
    public static readonly Dictionary<CardType, string> GameObjects = new Dictionary<CardType, string>
        {
            {
                CardType.HP, "Hpfish"
            },
            {
                CardType.HP2, "Hp2fish2"
            },
            {
                CardType.King,  "king"
            },
            {
                CardType.Octosmash, "Octosmash"
            },
            {
                CardType.Octosmash2, "Octosmash2"
            },
            {
                CardType.Octosmash3, "Octosmash3"
            },
            {
                CardType.Random, "Random"
            },
            {
                CardType.Watch, "Watch"
            },
            {
                CardType.Watch2, "Watch2"
            },
            {
                CardType.Aquaman, "Aquaman"
            }
        };
}