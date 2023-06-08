using System.Net.WebSockets;
using System.Threading.Channels;
using Chemistry.Utilities.Messages;
using Chemistry.Utilities;

namespace Chemistry.Core.Models
{
    public class FirstPartPresentation
    {
        private Shared shared;

        public FirstPartPresentation()
        {
            shared = new Shared();
        }

        public void FirstPartPlay(string language)
        {
            Console.Title = "Желязо/Fer";

            shared.LoadingAnimation();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres1, OutputMessages.BG_FirstPartPres1);
            Console.Clear();
            shared.PeriodicTable();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres2, OutputMessages.BG_FirstPartPres2);
            Thread.Sleep(700);
            Console.Clear();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres3, OutputMessages.BG_FirstPartPres3);
            shared.ElectronicStructure();

            Console.WriteLine();
            Console.WriteLine();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres4, OutputMessages.BG_FirstPartPres4);
            Console.Clear();

            shared.CorrosionQuiz(language);

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres5, OutputMessages.BG_FirstPartPres5);

            Console.WriteLine();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres6, OutputMessages.BG_FirstPartPres6);
            Thread.Sleep(500);
            Console.Clear();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPres7, OutputMessages.BG_FirstPartPres7);
            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}
