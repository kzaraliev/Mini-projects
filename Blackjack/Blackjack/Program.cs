using static System.Net.Mime.MediaTypeNames;

namespace Blackjack;

public class Program
{
    static void Main(string[] args)
    {
        Console.SetWindowSize(100, 25);
        Console.CursorVisible = false;
        Console.Title = "Blackjack";
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;

        Controller controller = new Controller();

        controller.Run();
    }
}