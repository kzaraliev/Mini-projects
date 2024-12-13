using Blackjack.Enum;
using Blackjack.Utilities;

namespace Blackjack.Entities;

public class Player
{
    public Player()
    {
        card = new Card();
        shared = new Shared();
        writer = new Writer();
        cardsValues = new List<int>();
        helperOfPoints = new List<int>();
        leftCardPossition = Constrants.leftForFirstCardsPrint;
    }

    private Card card;
    private List<int> cardsValues;
    private Shared shared;
    private Writer writer;
    public int Points { get; set; }
    private int leftCardPossition;
    private List<int> helperOfPoints;


    public void TakeFirstCards()
    {
        HelperForPoints(card.GenerateCard(Side.Open, leftCardPossition, Constrants.topPlayerCard));
        HelperForPoints(card.GenerateCard(Side.Open, leftCardPossition += Constrants.leftAdd, Constrants.topPlayerCard));
        shared.IsThereAce(cardsValues, Points);
        Points = cardsValues.Sum();
    }

    public bool PlayTurn()
    {
        //Player turn
        bool isStaying = true;

        string command = shared.AnimationForHorS(Constrants.HitOrStand);

        if (command == "H")
        {
            HelperForPoints(card.GenerateCard(Side.Open, leftCardPossition += Constrants.leftAdd, Constrants.topPlayerCard));
            cardsValues = shared.IsThereAce(cardsValues, Points);
            Points = cardsValues.Sum();
            Console.SetCursorPosition(Constrants.playerPointsNumberLeft, Constrants.playerPointsNumberTop);
            writer.Write($"{Points}");
        }
        else
        {
            isStaying = false;
        }

        return isStaying;
    }

    private void HelperForPoints(int values)
    {
        helperOfPoints.Add(values);
        cardsValues.Add(helperOfPoints[helperOfPoints.Count - 1]);
        Points += helperOfPoints[helperOfPoints.Count - 1];
    }
}