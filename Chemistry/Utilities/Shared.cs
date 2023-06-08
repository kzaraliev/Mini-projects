using Chemistry.IO;
using System.Text.RegularExpressions;
using Chemistry.Utilities.Messages;

namespace Chemistry.Utilities
{
    public class Shared
    {
        Writer writer;
        Reader reader;

        public Shared()
        {
            writer = new Writer();
            reader = new Reader();
        }

        public void PlayVideo(string language)
        {
            PrintFrOrBgMessage(language, OutputMessages.FR_SecondPartPres4, OutputMessages.BG_SecondPartPres4);
            Console.CursorVisible = true;
            
            string input = Console.ReadLine().ToUpper();
            Console.CursorVisible = false;

            if (input == "SKIP")
            {
                PrintFrOrBgMessage(language, "Vous manquez beaucoup de choses! : |", "Много пропускате! : |");
                Thread.Sleep(100);
                Console.Clear();
                return;
            }

            var uri = OutputMessages.VideoLink;
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
            Console.Clear();
            HighlightAnimation(OutputMessages.PressEnterVideo, ConsoleKey.Enter);
            Console.Clear();
        }

        public void LoadingAnimation()
        {
            writer.WriteLine("Loading...");
            Console.SetCursorPosition(7, 0);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("   ");
                Thread.Sleep(300);
                Console.SetCursorPosition(7, 0);
                writer.WriteLine("...");
                Console.SetCursorPosition(7, 0);
            }

            Console.Clear();
        }

        public void MatrixBinaryCode()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write("Program.exe running");

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            
            Random random = new Random();

            for (int i = 0; i < 105000; i++)
            {
                Console.Write(random.Next(2));
            }

            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
        }

        public void PrintFrOrBgMessage(string language, string frenchMessage, string bulgarianMessage)
        {
            if (language == "BG")
            {
                writer.WriteLine(bulgarianMessage);
            }
            else if (language == "FR")
            {
                writer.WriteLine(frenchMessage);
            }
        }

        public string GetInput()
        {
            Console.CursorVisible = true;
            string input = reader.ReadLine().ToUpper();
            Console.CursorVisible = false;
            Console.Clear();

            return input;
        }

        public void ElectronicStructure()
        {
            int atomicNumber = 26; // Atomic number of Iron
            int[] electronsPerLevel = { 2, 8, 14, 2 }; // Electrons per energy level

            Console.WriteLine();

            for (int i = 0; i < electronsPerLevel.Length; i++)
            {
                int energyLevel = i + 1;
                string orbitalType = GetOrbitalType(i);
                int electrons = electronsPerLevel[i];

                Console.Write($"  {energyLevel}{orbitalType}: ");
                for (int j = 1; j <= electrons; j++)
                {
                    Console.Write("•");
                }

                Console.WriteLine();
            }
        }

        public string GetOrbitalType(int energyLevel)
        {
            switch (energyLevel)
            {
                case 0: return "s";
                case 1: return "s";
                case 2: return "p";
                case 3: return "p";
                default: return "";
            }
        }

        public void PeriodicTable()
        {
            // Define an array of B group metals
            string[] bGroupMetals =
            {
            "Sc", "Ti", "V", "Cr", "Mn", "Co", "Ni", "Cu", "Zn", "Y", "Zr", "Nb", "Mo", "Tc", "Ru", "Rh", "Pd",
            "Ag", "Cd", "La", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg"
        };

            string Fe = "Fe";

            // Define an array of elements in the periodic table
            string[,] periodicTable = new string[10, 18]
            {
            { "H", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "He" },
            { "Li", "Be", "", "", "", "", "", "", "", "", "", "", "B", "C", "N", "O", "F", "Ne" },
            { "Na", "Mg", "", "", "", "", "", "", "", "", "", "", "Al", "Si", "P", "S", "Cl", "Ar" },
            {
                "K", "Ca", "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn", "Ga", "Ge", "As", "Se", "Br", "Kr"
            },
            {
                "Rb", "Sr", "Y", "Zr", "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn", "Sb", "Te", "I", "Xe"
            },
            {
                "Cs", "Ba", "La", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg", "Tl", "Pb", "Bi", "Po", "At",
                "Rn"
            },
            {
                "Fr", "Ra", "Ac", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn", "Nh", "Fl", "Mc", "Lv", "Ts",
                "Og"
            },
            { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
            { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }
            };


            // Loop through the rows and columns of the periodic table
            for (int row = 0; row < periodicTable.GetLength(0); row++)
            {
                for (int col = 0; col < periodicTable.GetLength(1); col++)
                {
                    string element = periodicTable[row, col];
                    Console.Write(element.PadRight(3));

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Thread.Sleep(500);
            writer.WriteLine(OutputMessages.PeriodicTableRules);

            MovingInPeriodicTable();

            Console.CursorVisible = false;

            for (int row = 0; row < periodicTable.GetLength(0); row++)
            {
                for (int col = 0; col < periodicTable.GetLength(1); col++)
                {
                    string element = periodicTable[row, col];

                    // Check if the element is in the B group metals array
                    if (Array.IndexOf(bGroupMetals, element) >= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }

                    if (element == Fe)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.Write(element.PadRight(3));

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Thread.Sleep(700);
        }

        private void MovingInPeriodicTable()
        {
            int left = 0;
            int top = 0;

            int topMax = 7;
            int leftMax = 53;

            int leftForEnter = 71;
            int topForEnter = 10;

            Console.SetCursorPosition(left, top);
            Console.CursorVisible = true;

            int leftDiff = 0;
            int topDiff = 0;

            while (true)
            {
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.LeftArrow)
                    {
                        leftDiff -= 1;
                        break;
                    }
                    else if (cki.Key == ConsoleKey.RightArrow)
                    {
                        leftDiff += 1;
                        break;
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        topDiff -= 1;
                        break;
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        topDiff += 1;
                        break;
                    }
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Console.CursorVisible = false;

                        Console.SetCursorPosition(leftForEnter, topForEnter);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(ConsoleKey.Enter.ToString());

                        Thread.Sleep(100);

                        Console.SetCursorPosition(leftForEnter, topForEnter);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("Enter");
                        Thread.Sleep(200);


                        if ((left == 21 || left == 22) && top == 3)
                        {
                            Console.Clear();
                            return;
                        }

                        break;
                    }
                } while (true);

                if (left + leftDiff < 0 || left + leftDiff >= leftMax || top + topDiff < 0 || top + topDiff >= topMax)
                {
                    leftDiff = 0;
                    topDiff = 0;
                    continue;
                }

                left += leftDiff;
                top += topDiff;

                Console.SetCursorPosition(left, top);
                Console.CursorVisible = true;

                leftDiff = 0;
                topDiff = 0;
            }
        }

        public void BeepMelody()
        {
            int[] notes = { 659, 659, 0, 659, 0, 523, 659, 0, 784, 392 };
            int[] duration = { 125, 125, 125, 125, 125, 125, 125, 125, 250, 250, 125, 125, 125, 250 };

            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i] == 0)
                {
                    Thread.Sleep(duration[i]);
                }
                else
                {
                    Console.Beep(notes[i], duration[i]);
                }
            }
        }

        public void HighlightAnimation(string message, ConsoleKey keyToHighlight)
        {
            Console.Write(message);
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
        }

        public void CorrosionQuiz(string language)
        {
            PrintFrOrBgMessage(language, OutputMessages.FR_FirstPartPresCorrosionPart, OutputMessages.BG_FirstPartPresCorrosionPart);
            Console.CursorVisible = true;

            string input = Console.ReadLine().ToUpper();
            Console.CursorVisible = false;

            Console.Clear();

            if (input == "КОРОЗИЯ" || input == "CORROSION")
            {
                writer.WriteLine("Bravo!");
            }
            else
            {
                PrintFrOrBgMessage(language, ExceptionMessages.FR_WrongAnswer2, ExceptionMessages.BG_WrongAnswer2);
            }
            Thread.Sleep(100);

            Console.Clear();
        }
    }
}
