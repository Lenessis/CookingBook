using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Pastel;

namespace CookingBook
{
    class Functions
    {

        public static string roseColor = "#FFC2B4";
        public static string nudeColor = "#A56F63";
        public static string yellowColor = "#F2E294";
        public static string default_pathfile = "./../../../Recipes/";

        public static void CenterText(string s)
        {
            WriteLine(String.Format("{0," + (((WindowWidth / 2)) + (s.Length / 2)) + "}", s));
        }

        /* --- Metoda odpowiadajaca za edycje tekstu --- */
        public static string ReadLine(string Default)
        {
            int pos = Console.CursorLeft;
            Console.Write(Default);
            ConsoleKeyInfo info;
            List<char> chars = new List<char>();
            //int position = chars.Count -1;

            if (string.IsNullOrEmpty(Default) == false)
            {
                chars.AddRange(Default.ToCharArray());
            }

            while (true)
            { 
                CursorVisible = true;
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Backspace && Console.CursorLeft > pos)
                {
                    chars.RemoveAt(chars.Count - 1);
                    Console.CursorLeft -= 1;
                    Console.Write(' ');
                    Console.CursorLeft -= 1;
                    //position--;

                }
                else if (info.Key == ConsoleKey.Spacebar)
                {
                    //chars.Insert(position, ' ');
                    chars.Add(' ');
                    Console.Write(' ');
                    //position++;
                }
                /*else if (info.Key == ConsoleKey.LeftArrow)
                {
                    if (CursorLeft > 0)
                    {
                        CursorLeft--;
                        //position--;
                    }
                       
                }
                else if (info.Key == ConsoleKey.RightArrow)
                {
                    CursorLeft++;
                   // position++;
                }
                else if (info.Key == ConsoleKey.UpArrow)
                {
                    if (CursorLeft > 0)
                    {
                        CursorLeft--;
                       // position--;
                    }
                }
                else if (info.Key == ConsoleKey.DownArrow)
                {
                    CursorTop++;
                    //position++;
                }*/
                else if (info.Key == ConsoleKey.Enter)
                {
                    //chars.Insert(position, '\n');
                    chars.Add('\n');
                    Console.Write('\n');
                    //position++;
                }

                else if (info.Key == ConsoleKey.F2)
                {
                    Console.Write(Environment.NewLine); break;
                }

                //Here you need create own checking of symbols
                else if (char.IsLetterOrDigit(info.KeyChar))
                {
                    Console.Write(info.KeyChar);
                    chars.Add(info.KeyChar);
                    /*chars.Insert(position, info.KeyChar);
                    position++;*/
                }
                
                else if (char.IsPunctuation(info.KeyChar))
                {
                    Console.Write(info.KeyChar);
                    chars.Add(info.KeyChar);
                    /*chars.Insert(position, info.KeyChar);
                    position++;*/
                }
            }
            return new string(chars.ToArray());
        }

        public static void Clearing(int x, int y, int width, int height)
        {
            CursorVisible = false;
            SetCursorPosition(x, y);
            for (int i = 1; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Write(" ");
                }
                WriteLine();               
            }
            //CursorVisible = true;
        }

        public static void DrawFrame(int x, int y, int width, int height)
        {
            SetCursorPosition(x, y);
            for (int i = 0; i < width; i++)
            {
                Write("_".Pastel(roseColor));
            }
            SetCursorPosition(x, y + 1);
            for (int i = 0; i < height; i++)
            {
                WriteLine("|".Pastel(roseColor));
            }
            SetCursorPosition(x+width, y + 1);
            for (int i = 0; i < height; i++)
            {
                WriteLine("|".Pastel(roseColor));
            }
            SetCursorPosition(x, y+height);
            for (int i = 0; i < width; i++)
            {
                Write("_".Pastel(roseColor));
            }

           // Clearing(x + 1, y + 1, width - 1, height - 1);
        }

        public static void DrawFrame(int x, int y, int width, int height, string name) 
        {
            SetCursorPosition(x, y);
            for (int i = 0; i < width; i++)
            {
                Write("-".Pastel(roseColor));
            }
            
            for (int i = 1; i <= height; i++)
            {
                SetCursorPosition(x, y + i);
                Write("|".Pastel(roseColor));
                SetCursorPosition(x + width, y + i);
                Write("|".Pastel(roseColor));
            }
            /*SetCursorPosition(x + width, y + 1);
            for (int i = 0; i < height; i++)
            {
                WriteLine("|".Pastel(roseColor));
            }*/
            SetCursorPosition(x, y + height);
            for (int i = 0; i < width; i++)
            {
                Write("-".Pastel(roseColor));
            }

            SetCursorPosition(x + 1, y);
                Write($"** {name} **");

            //Clearing(x + 1, y + 1, width - 1, height - 1);
        }


    }
}
