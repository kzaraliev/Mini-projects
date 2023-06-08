using Chemistry.IO;
using Chemistry.Utilities.Messages;
using Chemistry.Utilities;

namespace Chemistry.Core.Models
{
    public class Intro
    {
        private Shared shared;
        private Writer writer;

        public Intro()
        {
            shared = new Shared();
            writer = new Writer();
        }

        public string IntroPlay()
        {
            Console.CursorVisible = false;
            shared.BeepMelody();
            shared.LoadingAnimation();
            writer.WriteLine(OutputMessages.WelcomeMessage);
            Console.Clear();
            writer.WriteLine(OutputMessages.LanguageOnConsole);
            string languageForConsole = shared.GetInput();

            bool flag = true;
            while (flag)
            {
                flag = IsValidLanguageForConsole(languageForConsole);
                if (flag)
                {
                    writer.WriteLine(ExceptionMessages.InvalidLanguage);
                    Console.Clear();
                    Console.Write("BG/FR: System.Console.Language.");
                    languageForConsole = shared.GetInput();
                }
            }

            if (languageForConsole == "FR" || languageForConsole == "ФР")
            {
                return "FR";
            }
            else
            {
                return "BG";
            }
        }

        private bool IsValidLanguageForConsole(string language)
        {
            if (language == "FR" || language == "ФР" || language == "BG" || language == "БГ")
            {
                return false;
            }

            return true;
        }
    }
}
