using Blackjack.Enum;
using Blackjack.Utilities;

namespace Blackjack.Entities;

public class Computer
{
    public Computer()
    {
        card = new Card();
        writer = new Writer();
        shared = new Shared();
        cardsValues = new List<int>();
        helperOfPoints = new List<int>();
        leftCardPossition = Constrants.leftForFirstCardsPrint;
    }

    private Card card;
    private Writer writer;
    private Shared shared;
    private List<int> helperOfPoints;
    private List<int> cardsValues;
    public int Points { get; set; }
    private int leftCardPossition;
    private bool isCardReversed = false;

    public void TakeFirstCard()
    {
        //This is not necessary for the points in game(just for print on console)
        card.GenerateCard(Side.Close, leftCardPossition, Constrants.topCompCard);
        HelperForPoints(card.GenerateCard(Side.Open, leftCardPossition += Constrants.leftAdd, Constrants.topCompCard));
    }

    public bool PlayTurn(int pointsOfPlayer)
    {
        if (!isCardReversed)
        {
            HelperForPoints(card.GenerateCard(Side.Reverse, Constrants.leftForFirstCardsPrint, Constrants.topCompCard));
            shared.IsThereAce(cardsValues, Points);
            Points = cardsValues.Sum();
            Console.SetCursorPosition(Constrants.compPointsNumberLeft, Constrants.compPointsNumberTop);
            writer.Write($"{Points}");
            isCardReversed = true;
        }
        
        if (isNecessaryToHit(pointsOfPlayer))
        {
            HelperForPoints(card.GenerateCard(Side.Open, leftCardPossition += Constrants.leftAdd, Constrants.topCompCard));
            cardsValues = shared.IsThereAce(cardsValues, Points);
            Points = cardsValues.Sum();
            Console.SetCursorPosition(Constrants.compPointsNumberLeft, Constrants.compPointsNumberTop);
            writer.Write($"{Points}");
            return true;
        }

        return false;
    }

    private bool isNecessaryToHit(int pointsOfPlayer)
    {
        if (pointsOfPlayer >= Points)
        {
            return true;
        }

        return false;
    }

    private void HelperForPoints(int values)
    {
        helperOfPoints.Add(values);
        cardsValues.Add(helperOfPoints[helperOfPoints.Count - 1]);
        Points += helperOfPoints[helperOfPoints.Count - 1];
    }
}