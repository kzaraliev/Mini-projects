using Chemistry.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chemistry.Utilities.Messages;

namespace Chemistry.Core.Models
{
    public class SecondPartPresentation
    {
        private Shared shared;

        public SecondPartPresentation()
        {
            shared = new Shared();
        }

        public void SecondPartPlay(string language)
        {
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_SecondPartPres1, OutputMessages.BG_SecondPartPres1);
            Console.WriteLine();
            Console.WriteLine();
            shared.HighlightAnimation(OutputMessages.PressEnter, ConsoleKey.Enter);
            Thread.Sleep(500);
            Console.Clear();
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_SecondPartPres2, OutputMessages.BG_SecondPartPres2);
            Thread.Sleep(500);
            Console.Clear();
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_SecondPartPres3, OutputMessages.BG_SecondPartPres3);
            Console.Clear();
            shared.PlayVideo(language);
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_SecondPartPresFinalWords, OutputMessages.BG_SecondPartPresFinalWords);
            Thread.Sleep(2000);
        }
    }
}
