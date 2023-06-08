namespace Chemistry.IO;

using System;
using Chemistry.IO.Contracts;

public class Reader : IReader
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}