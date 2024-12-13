using Blackjack.Enum;
using Blackjack.Utilities;

namespace Blackjack.Entities;

public class Area
{
    public Area()
    {
        writer = new Writer();
        shared = new Shared();
    }

    private Writer writer;
    private Shared shared;

    public void DrawArea()
    {
        int leftForMiddleLine = Console.WindowLeft + 5;
        int topForMiddleLine = Console.WindowWidth / 10;

        Console.SetCursorPosition(leftForMiddleLine, topForMiddleLine);
        for (int i = 0; i < Console.WindowWidth - 10; i++)
        {
            writer.Write("_");
        }

        int leftForComputerText = Console.WindowLeft + 8;
        int topForComputerText = 1;

        Console.SetCursorPosition(leftForComputerText, topForComputerText);
        writer.Write("Koceto Cards:");

        int leftForPlayerText = Console.WindowLeft + 8;
        int topForPlayerText = 12;

        Console.SetCursorPosition(leftForPlayerText, topForPlayerText);
        writer.Write("Your Cards:");

        int leftForAlert = Console.WindowLeft + 38;
        int topForAlert = 10;

        Console.SetCursorPosition(leftForAlert, topForAlert);

        shared.HighlightAnimation("Press Enter to Start", ConsoleKey.Enter);
    }

    public void DrawPoints(int compPoints, int playerPoints)
    {
        Console.SetCursorPosition(Constrants.compPointsTextLeft, Constrants.compPointsTextTop);
        writer.Write("Points: ");
        writer.Write($"{compPoints}");

        Console.SetCursorPosition(Constrants.playerPointsTextLeft, Constrants.playerPointsTextTop);
        writer.Write("Points: ");
        writer.Write($"{playerPoints}");
    }

    public bool DisplayMessage(string? blackjack, string? bust, string message)
    {
        bool flag = true;
        int left = Console.WindowLeft + 45;
        int top = Console.WindowWidth / 10;

        int leftForAlert = Console.WindowLeft + 38;
        int topForAlert = 10;

        if (blackjack != null)
        {
            Console.SetCursorPosition(left, top);
            writer.Write(blackjack);
            Console.SetCursorPosition(left, top);
            Thread.Sleep(1500);
            for (int i = 0; i < 10; i++)
            {
                writer.Write("_");
            }
        }

        if (bust != null)
        {
            Console.SetCursorPosition(left, top);
            writer.Write(bust);
            Console.SetCursorPosition(left, top);
            Thread.Sleep(1500);
            for (int i = 0; i < 10; i++)
            {
                writer.Write("_");
            }
        }

        Console.SetCursorPosition(left - 6, top);
        writer.Write(message);

        Thread.Sleep(1000);

        Console.SetCursorPosition(leftForAlert, topForAlert);
        for (int i = 0; i < 30; i++)
        {
            writer.Write("_");
        }

        return flag;
    }

    public void ClearOldInformation()
    {
        for (int i = 0; i <= 6; i++)
        {
            Console.SetCursorPosition(Constrants.leftForFirstCardsPrint, Constrants.topCompCard + i);
            Console.Write("                                                          ");
        }

        for (int i = 0; i <= 8; i++)
        {
            Console.SetCursorPosition(Constrants.leftForFirstCardsPrint, Constrants.topPlayerCard + i);
            Console.Write("                                                          ");
        }

        Console.SetCursorPosition(Constrants.compPointsTextLeft, Constrants.compPointsTextTop);
        Console.Write("          ");

        Console.SetCursorPosition(Constrants.playerPointsTextLeft, Constrants.playerPointsTextTop);
        Console.Write("          ");
    }
}