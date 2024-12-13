using System.Text.RegularExpressions;

namespace Blackjack.Utilities;

public class Shared
{
    public Shared()
    {
        writer = new Writer();
    }

    private Writer writer;

    public List<int> IsThereAce(List<int> cardsValues, int points)
    {
        for (int i = 0; i < cardsValues.Count; i++)
        {
            if ((cardsValues[i] == 11) && points > 21)
            {
                cardsValues[i] = 1;
            }
        }

        return cardsValues;
    }
    public void HighlightAnimation(string message, ConsoleKey keyToHighlight)
    {
        writer.Write(message);
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        string wordToHighlight = keyToHighlight.ToString();
        int lengthOfWord = wordToHighlight.Length;

        string[] splitedMessage = Regex.Split(message, "( )");


        int leftToRemove = 0;
        if (splitedMessage.Contains(wordToHighlight))
        {
            for (int i = 0; i < splitedMessage.Length; i++)
            {
                if (splitedMessage[i] == wordToHighlight)
                {
                    for (int j = splitedMessage.Length - 1; j >= i; j--)
                    {
                        leftToRemove += splitedMessage[j].Length;
                    }
                }
            }
        }
        else
        {
            throw new ArgumentException("The text does not contain the special symbol for the animation");
        }

        left -= leftToRemove;

        ConsoleKeyInfo cki;
        do
        {
            cki = Console.ReadKey(true);
            if (cki.Key == keyToHighlight)
            {
                Console.SetCursorPosition(left, top);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(keyToHighlight.ToString());
                break;
            }
        } while (true);

        Thread.Sleep(100);

        Console.SetCursorPosition(Console.CursorLeft - lengthOfWord, Console.CursorTop);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(wordToHighlight);
        Thread.Sleep(200);

        Console.SetCursorPosition(left - 8, top);

        for (int i = 0; i < 22; i++)
        {
            writer.Write("_");
        }
    }

    public string AnimationForHorS(string message)
    {
        Console.SetCursorPosition(Constrants.leftForHorS, Constrants.topForHorS);

        writer.Write(message);
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        string hit = "(H)it";
        string stand = "(S)tand";

        ConsoleKeyInfo cki;
        do
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.H)
            {
                Console.SetCursorPosition(left - 16, top);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(hit);
                Thread.Sleep(100);

                Console.SetCursorPosition(Console.CursorLeft - hit.Length, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(hit);
                Thread.Sleep(200);
                break;
            }
            else if (cki.Key == ConsoleKey.S)
            {
                Console.SetCursorPosition(left - 7, top);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(stand);

                Thread.Sleep(100);

                Console.SetCursorPosition(Console.CursorLeft - stand.Length, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(stand);
                Thread.Sleep(200);
                break;
            }
        } while (true);

        return cki.Key.ToString();
    }

    public string AnimationForReplayOrLeave(string message)
    {
        int leftForAlert = Console.WindowLeft + 40;
        int topForAlert = 10;

        Console.SetCursorPosition(leftForAlert, topForAlert);

        writer.Write(message);
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        string replay = "(R)eplay";
        string escape = "(Esc)ape";

        ConsoleKeyInfo cki;
        do
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.R)
            {
                Console.SetCursorPosition(left - message.Length, top);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(replay);
                Thread.Sleep(100);

                Console.SetCursorPosition(Console.CursorLeft - replay.Length, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(replay);
                Thread.Sleep(200);
                break;
            }
            else if (cki.Key == ConsoleKey.Escape)
            {
                Console.SetCursorPosition(left - escape.Length, top);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(escape);

                Thread.Sleep(100);

                Console.SetCursorPosition(Console.CursorLeft - escape.Length, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(escape);
                Thread.Sleep(200);
                break;
            }
        } while (true);

        Console.SetCursorPosition(leftForAlert, topForAlert);
        for (int i = 0; i < 23; i++)
        {
            writer.Write("_");
        }

        return cki.Key.ToString();
    }

    public void FinalMessage()
    {
        Thread.Sleep(400);

        int leftForAlert = Console.WindowLeft + 40;
        int topForAlert = 10;

        Console.SetCursorPosition(leftForAlert, topForAlert);

        writer.Write(Constrants.FinalMessage1);

        Thread.Sleep(1000);

        Console.SetCursorPosition(leftForAlert, topForAlert);
        for (int i = 0; i < 23; i++)
        {
            writer.Write("_");
        }

        Thread.Sleep(100);

        Console.SetCursorPosition(leftForAlert + 5, topForAlert);
        writer.Write(Constrants.FinalMessage2);

        Thread.Sleep(2000);
    }
}