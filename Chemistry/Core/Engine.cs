// ReSharper disable InconsistentNaming
// ReSharper disable FunctionNeverReturns

using CarRacing.Core;
using CarRacing.Core.Contracts;
using Chemistry.Core.Models;
using Chemistry.IO;
using Chemistry.IO.Contracts;
using Chemistry.Utilities.Messages;

namespace Chemistry.Core;
using System;
using System.Text;

public class Engine : IEngine
{
    private readonly IWriter writer;
    private readonly IReader reader;
    private readonly Controller controller;

    public Engine()
    {
        this.writer = new Writer();
        this.reader = new Reader();
        this.controller = new Controller();
    }

    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Title = "Метал по избор";

        //https://www.youtube.com/watch?v=RcT9L71vLHk - kurvqshto jelqzo
        //https://www.youtube.com/watch?v=GTvE8yT-I4g&t=68s - jelezen vulkan

       controller.Control(nameof(Intro));
       controller.Control(nameof(FirstGame));
       controller.Control(nameof(FirstPartPresentation));
       controller.Control(nameof(SecondPartPresentation));
    }
}
