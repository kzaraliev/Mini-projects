namespace Chemistry.IO;

using System;
using Chemistry.IO.Contracts;

public class Writer : IWriter
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        char[] charsForPrint = message.ToCharArray();
        
        for (int i = 0; i < charsForPrint.Length; i++)
        {
            if (charsForPrint[i] == '2' && charsForPrint[i + 1] == '6' ||
                charsForPrint[i] == 'F' && charsForPrint[i + 1] == 'e' ||
                charsForPrint[i] == '8' && charsForPrint[i + 1] == ' ' || charsForPrint[i] == '4' ||
                charsForPrint[i] == '+' || charsForPrint[i] == 'C' && charsForPrint[i + 1] == 'l' ||
                charsForPrint[i] == 'l' && charsForPrint[i + 1] == '₃' ||
                charsForPrint[i] == 'O' && charsForPrint[i + 1] == '₄' ||
                charsForPrint[i] == 'S' && charsForPrint[i + 1] == 'O' ||
                charsForPrint[i] == '7' && charsForPrint[i + 1] == 'H' ||
                charsForPrint[i] == '₂' && charsForPrint[i + 1] == 'O' ||
                charsForPrint[i] == '2' && charsForPrint[i + 1] == 'F' ||
                charsForPrint[i] == '²' && charsForPrint[i + 1] == '⁺' ||
                charsForPrint[i] == 'e' && charsForPrint[i + 1] == '⁻' ||
                charsForPrint[i] == '-' && charsForPrint[i + 1] == '>' ||
                charsForPrint[i] == 'O' && charsForPrint[i + 1] == '₂' ||
                charsForPrint[i] == 'H' && charsForPrint[i + 1] == '⁻' ||
                charsForPrint[i] == 'n' && charsForPrint[i + 1] == 'H' ||
                charsForPrint[i] == '2' && charsForPrint[i + 1] == 'H' ||
                charsForPrint[i] == 'e' && charsForPrint[i + 1] == '₃' ||
                charsForPrint[i] == 'O' && charsForPrint[i + 1] == '₃' ||
                charsForPrint[i] == 'C' && charsForPrint[i + 1] == 'O' ||
                charsForPrint[i] == 'P' && charsForPrint[i + 1] == 'O' ||
                charsForPrint[i] == '8' && charsForPrint[i + 1] == 'H' ||
                charsForPrint[i] == '2' && charsForPrint[i + 1] == '•' ||
                charsForPrint[i] == '7' && charsForPrint[i + 1] == '0' ||
                charsForPrint[i] == '0' && charsForPrint[i + 1] == '%' ||
                charsForPrint[i] == '9' && charsForPrint[i + 1] == '5' ||
                charsForPrint[i] == '5' && charsForPrint[i + 1] == '%'
                )
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
        
                Console.Write(charsForPrint[i]);
                Thread.Sleep(70);
            }
            else
            {
                Console.Write(charsForPrint[i]);
                Thread.Sleep(70);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        
        Thread.Sleep(300);
    }
}