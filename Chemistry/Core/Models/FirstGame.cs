using Chemistry.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chemistry.Utilities;
using Chemistry.IO;

namespace Chemistry.Core.Models
{
    public class FirstGame
    {
        private Shared shared;
        private Writer writer;

        public FirstGame()
        {
            shared = new Shared();
            writer = new Writer();
        }

        public void FirstGameAlert(string language)
        {
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_GameAlert, OutputMessages.BG_GameAlert);

            IsGamePlaying(language);
        }

        private void IsGamePlaying(string language)
        {
            string playingOrSkip = shared.GetInput();

            bool flag = true;
            while (flag)
            {
                flag = IsValidGameCommand(playingOrSkip);
                if (flag)
                {
                    writer.WriteLine(ExceptionMessages.InvalidGameCommand);
                    Console.Clear();
                    Console.Write("Play/Skip: System.");
                    playingOrSkip = shared.GetInput();
                }
            }

            if (playingOrSkip == "PLAY")
            {
                PlayGameFirstGame(language);
            }

        }

        private void PlayGameFirstGame(string language)
        {
            shared.LoadingAnimation();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstGame1, OutputMessages.BG_FirstGame1);
            Console.Clear();
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstGame2, OutputMessages.BG_FirstGame2);

            string answer = shared.GetInput();

            if (CheckAnswerFirstGame(answer, language))
            {
                return;
            }

            shared.PrintFrOrBgMessage(language, ExceptionMessages.FR_WrongAnswer1, ExceptionMessages.BG_WrongAnswer1);

            Console.Clear();

            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstGame3, OutputMessages.BG_FirstGame3);
            Console.Clear();
            shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstGame4, OutputMessages.BG_FirstGame4);

            string secondAnswer = shared.GetInput();

            if (CheckAnswerFirstGame(secondAnswer, language))
            {
                return;
            }

            shared.PrintFrOrBgMessage(language, ExceptionMessages.FR_WrongAnswer2, ExceptionMessages.BG_WrongAnswer2);
            Console.Clear();
        }

        private bool IsValidGameCommand(string command)
        {
            if (command == "PLAY" || command == "SKIP")
            {
                return false;
            }

            return true;
        }

        private bool CheckAnswerFirstGame(string answer, string language)
        {
            if (answer == "ЖЕЛЯЗО" || answer == "FER")
            {
                shared.PrintFrOrBgMessage(language, OutputMessages.FR_FirstGameRightAnswer,
                    OutputMessages.BG_FirstGameRightAnswer);

                Console.Clear();
                return true;
            }

            return false;
        }
    }
}
