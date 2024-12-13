using Blackjack.Enum;
using Blackjack.Utilities;

namespace Blackjack.Entities;

public class Card
{
    public Card()
    {
        random = new Random();
        writer = new Writer();
        allCardsTypes = new Dictionary<string, int>()
        {
            {"2", 2},
            { "3",  3 },
            { "4",  4 },
            { "5",  5 },
            { "6",  6 },
            { "7",  7 },
            { "8",  8 },
            { "9",  9 },
            { "10", 10},
            { "J",  10},
            { "Q",  10},
            { "K",  10},
            { "A",  11}
        };
    }

    private readonly Random random;
    private Writer writer;
    private Dictionary<string, int> allCardsTypes;

    public int GenerateCard(Side side, int left, int top)
    {
        var cardType = random.Next(0, 13);
        var cardSuit = (CardSuit)random.Next(0, 4);

        if (side == Side.Close)
        {
            DrawReverseCard(left, top);
        }
        else if (side == Side.Open)
        {
            DrawCard(cardSuit, cardType, left, top);
        }
        else
        {
            DrawReverseCardOpen(cardSuit, cardType, left, top);
        }
            
        return allCardsTypes.ElementAt(cardType).Value;
    }

    private void DrawReverseCardOpen(CardSuit cardSuit, int cardType, int left, int top)
    {
        Console.SetCursorPosition(left, top);

        string suit = "";

        if (cardSuit == CardSuit.Club)
        {
            suit = "♣";
        }
        else if (cardSuit == CardSuit.Diamond)
        {
            suit = "♦";
        }
        else if (cardSuit == CardSuit.Heart)
        {
            suit = "♥";
        }
        else if (cardSuit == CardSuit.Spade)
        {
            suit = "♠";
        }

        writer.Write(@"•-–---");
        Console.SetCursorPosition(left, top + 1);

        writer.Write($"|{suit}");
        Console.SetCursorPosition(left, top + 2);

        writer.Write("| ");
        Console.SetCursorPosition(left, top + 3);

        if (allCardsTypes.ElementAt(cardType).Key == "10")
        {
            writer.Write($"|  {allCardsTypes.ElementAt(cardType).Key}");
        }
        else
        {
            writer.Write($"|   {allCardsTypes.ElementAt(cardType).Key}");
        }

        Console.SetCursorPosition(left, top + 4);

        writer.Write("| ");
        Console.SetCursorPosition(left, top + 5);

        writer.Write("| ");
        Console.SetCursorPosition(left, top + 6);

        writer.Write("•-–---");

    }

    private void DrawReverseCard(int left, int top)
    {
        Console.SetCursorPosition(left, top);

        writer.Write(@"•-–-----•");
        Console.SetCursorPosition(left, top + 1);

        writer.Write("|♦     ♣|");
        Console.SetCursorPosition(left, top + 2);

        writer.Write("|       |");
        Console.SetCursorPosition(left, top + 3);

        writer.Write("|   ?   |");

        Console.SetCursorPosition(left, top + 4);

        writer.Write("|       |");
        Console.SetCursorPosition(left, top + 5);

        writer.Write("|♠     ♥|");
        Console.SetCursorPosition(left, top + 6);

        writer.Write("•-–-----•");
    }

    private void DrawCard(CardSuit cardSuit, int cardType, int left, int top)
    {
        Console.SetCursorPosition(left, top);
        string suit = "";

        if (cardSuit == CardSuit.Club)
        {
            suit = "♣";
        }
        else if (cardSuit == CardSuit.Diamond)
        {
            suit = "♦";
        }
        else if (cardSuit == CardSuit.Heart)
        {
            suit = "♥";
        }
        else if (cardSuit == CardSuit.Spade)
        {
            suit = "♠";
        }


        writer.Write(@"•-–-----•");
        Console.SetCursorPosition(left, top + 1);

        writer.Write($"|{suit}      |");
        Console.SetCursorPosition(left, top + 2);

        writer.Write("|       |");
        Console.SetCursorPosition(left, top + 3);

        if (allCardsTypes.ElementAt(cardType).Key == "10")
        {
            writer.Write($"|  {allCardsTypes.ElementAt(cardType).Key}   |");
        }
        else
        {
            writer.Write($"|   {allCardsTypes.ElementAt(cardType).Key}   |");
        }
        Console.SetCursorPosition(left, top + 4);

        writer.Write("|       |");
        Console.SetCursorPosition(left, top + 5);

        writer.Write($"|      {suit}|");
        Console.SetCursorPosition(left, top + 6);

        writer.Write("•-–-----•");
    }
}