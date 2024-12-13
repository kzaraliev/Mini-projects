namespace Blackjack.Utilities;

public class Writer
{
    public void Write(string message)
    {
        char[] charsForPrint = message.ToCharArray();

        foreach (var ch in charsForPrint)
        {
            if (ch == '♥' || ch == '♦')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(ch);
                Thread.Sleep(25);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(ch);
                Thread.Sleep(25);
            }
        }
    }
}