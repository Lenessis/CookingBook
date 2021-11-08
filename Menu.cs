using Pastel; /* https://github.com/silkfire/Pastel */
using System;
using System.Collections.Generic;
using System.Drawing; // potrzebne do Pastel
using System.Text;
using static System.Console;

namespace CookingBook
{
    class Menu
    {
        /* --- ZMIENNE --- */

        private string[] options {get; set;}
        private int selectedIndex;
        private string prompt { get; set; } // -- tekst zachecajacy do wybrania opcji


        /* --- KONSTRUKTORY --- */

        public Menu() 
        {
            string[] list = { "Show list of recipes", "Add recipe", "Edit recipe", "Delete recipe", "Exit" };

            this.options = list;
            this.prompt = "Select an option and press ENTER...";
            this.selectedIndex = 0;
        }       

        public Menu( string prompt, string[] options)
        {
            this.options = options;
            this.prompt = prompt;
            this.selectedIndex = 0;
        }

        /* --- METODY --- */

        /* --- Wyświtlanie opcji menu --- */
        private void DisplayOptions(bool center)
        {
            SetCursorPosition(8, 9);
            Functions.CenterText(prompt.Pastel(Functions.roseColor));
            WriteLine();
            for (int i = 0; i<options.Length; i++)
            {
                string currentOption = options[i];
                string prefix ="";
                string foregroundColor;
                string backgroundColor;

                if (i == selectedIndex)
                {
                    
                    prefix = "-->";
                    foregroundColor = Functions.nudeColor;
                    backgroundColor = Functions.roseColor;
                }
                else
                {
                    prefix = "   ";
                    foregroundColor = Functions.roseColor;
                    backgroundColor = Functions.nudeColor;
                }

                string result = $"{prefix} ** {currentOption} **   ".Pastel(foregroundColor).PastelBg(backgroundColor);

                SetCursorPosition(16, 11+i);

                if(center==true)
                    Functions.CenterText(result);
                else
                    WriteLine($"{prefix} ** {currentOption} **".Pastel(foregroundColor).PastelBg(backgroundColor));
            }
            ResetColor();
        }


        public int Run()
        {
            ConsoleKey keyPressed;
            Clear();

            do
            {
                // Clear(); // zmienic na czyszczenie tylko wyznaczonego obszaru
                Functions.Clearing(10, 10, 40, 20);
                WriteTitle();
                DisplayOptions(true);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if(keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                        selectedIndex = options.Length - 1;
                }
                else if(keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length)
                        selectedIndex = 0;
                }
                else if(keyPressed == ConsoleKey.Escape)
                {
                    selectedIndex = -1;
                    break;
                }


            } while (keyPressed != ConsoleKey.Enter);

            return selectedIndex;
        }

        public static void WriteTitle()
        {//animacja filizanki
            SetCursorPosition(0, 0);
            WriteLine(@"
                                                                                            (  )   (   )  )
                                                                                             ) (   )  (  (
                                                                                             ( )  (    ) )
                                                                                             _____________
                                                                                            <_____________> ___
                                                                                            |             |/ _ \
                                                                                            |               | | |
                                                                                            |               |_| |
                                                                                         ___|             |\___/
                                                                                        /    \___________/    \
                                                                                        \_____________________/
".Pastel("#FFC2B4"));

            SetCursorPosition(0, 0);

            WriteLine(@"
                               _____               _     ____                 _    
                              / ____|             | |   |  _ \               | |        
                             | |      ___    ___  | | __| |_) |  ___    ___  | | __
                             | |     / _ \  / _ \ | |/ /|  _ <  / _ \  / _ \ | |/ /
                             | |____| (_) || (_) ||   < | |_) || (_) || (_) ||   < 
                              \_____|\___/  \___/ |_|\_\|____/  \___/  \___/ |_|\_\
                                                       
                                                       
".Pastel("#FFC2B4"));

        }

        
    }
}
