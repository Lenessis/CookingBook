using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Pastel;
using System.Drawing;
using System.IO;
using System.Linq;
using WMPLib;

namespace CookingBook
{
    class MainMenu
    {
        private List<string> file_name_recipe = new List<string>();
        

        /* -- Menu -- */

        public void StartMenu()
        {
            
            RunMainMenu();

        }

        private void RunMainMenu()
        {
            RecipesFilesFromDirectory();
            Menu mainMenu = new Menu();
            int selectedIndex = mainMenu.Run();

            switch(selectedIndex)
            {
                case 0:
                    ShowRecipesList();
                    break;
                case 1:
                    AddRecipe();
                    break;
                case 2:
                    EditRecipe();
                    break;
                case 3:
                    DeleteRecipe();
                    //usuwanie pliku na podstawie nazwy/indeksu 
                    break;
                case 4:
                    Exit();
                    break;
            }

        }
      

        private void ShowRecipesList()
        {
            Clear();
            Menu recepies = new Menu("", file_name_recipe.ToArray());
            int index = recepies.Run();

            Recipe recipe = new Recipe();
            string path = Functions.default_pathfile + file_name_recipe[index] + ".txt";
            recipe.ReadRecipeFromFile(path);
            recipe.ShowRecipe();

            SetCursorPosition(0, 35);

            ReturnToMenu();
        }

        private void AddRecipe()
        {
            Clear();

            Recipe recipe = new Recipe();
            recipe.AddView();

            ReturnToMenu();
        }

        private void EditRecipe()
        {
            Clear();

            Menu recepies = new Menu("", file_name_recipe.ToArray());
            int index = recepies.Run();

            Recipe recipe = new Recipe();
            string path = Functions.default_pathfile + file_name_recipe[index] + ".txt";
            recipe.ReadRecipeFromFile(path);
            recipe.EditView();

            SetCursorPosition(0, 35);
            ReturnToMenu();
        }

        private void DeleteRecipe()
        {
            Clear();

            if (file_name_recipe.Count==0)
                RecipesFilesFromDirectory();

            Menu recepies = new Menu("", file_name_recipe.ToArray());
            int index = recepies.Run();
            DeleteFilesFromDirectory(index);

            ReturnToMenu();
        }

        private void Exit()
        {
            Environment.Exit(0);
        }

        private void ReturnToMenu()
        {
            WriteLine();
            Functions.CenterText("\t\t Press any key to return to menu...".Pastel(Functions.roseColor).PastelBg(Functions.nudeColor));
            CursorVisible = false;
            ReadKey(false);
            RunMainMenu();
        }

        /* -- Directory --- */

        private void RecipesFilesFromDirectory()
        {
            file_name_recipe = Directory.GetFiles(Functions.default_pathfile).ToList();
            List<string> temp = new List<string>();

            foreach (var item in file_name_recipe)
            {
                string x = item.Replace(Functions.default_pathfile, "");               
                temp.Add(x.Replace(".txt", ""));
            }
                
            file_name_recipe = temp;

        } //czyta nazwy wszystkich plików z folderu domyślnego

        private void DeleteFilesFromDirectory(int index)
        {
            string trash = Functions.default_pathfile + file_name_recipe[index] + ".txt";

            File.Delete(trash);
            Functions.CenterText("Succesful!".Pastel("#A4E74F")); 

        }

        


    }
}
