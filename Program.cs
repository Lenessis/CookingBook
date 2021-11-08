using System;
using static System.Console;
using System.Media;


namespace CookingBook
{
    class Program
    {
        
        static void Main(string[] args)
        {
           
            /*SoundPlayer music = new SoundPlayer("silly_chicken.wav");
            music.Load();
            music.PlayLooping();*/


            MainMenu mainMenu = new MainMenu();
            mainMenu.StartMenu();
        }

        

    }
}
