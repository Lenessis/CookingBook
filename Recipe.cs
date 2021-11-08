using Pastel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;

namespace CookingBook
{
    class Recipe
    {
        private string nameRecipe;
        private List<string> ingredients = new List<string>();
        private string preparing;

        private static int xIngFramePosition = 2;
        private static int yIngFramePosition = 12;

        private static int xPreFramePosition = 40;
        private static int yPreFramePosition = 12;

        public Recipe()
        {
            preparing = "";
        }

        /* -- Recipe --*/

        private void View()
        {
            Menu.WriteTitle();

            Functions.DrawFrame(xIngFramePosition, yIngFramePosition, 38, 20, "INGREDIENTS");
            Functions.DrawFrame(xPreFramePosition, yPreFramePosition, 70,20,"PREPARING");
        }

        public void ShowRecipe()
        {
            Clear();
            View();
            ShowIngredients();
            ShowPreparing();
        }

        public void EditView()
        {
            Clear();
            
            Functions.Clearing(2, 11, 69, 19);
            OptionsMenu();
            Functions.DrawFrame(1, 10, 70, 20, "Preparing");
            SetCursorPosition(3, 12);
            EditPreparing();
            WriteRecipeToFile(Functions.default_pathfile);
        }
        public void AddView()
        {
            Clear();

            Menu.WriteTitle();

            Functions.DrawFrame(20, 10, 70, 6, "Set title");
            SetCursorPosition(22, 12);
            WriteLine("Enter the title of your dish: ".Pastel(Functions.yellowColor));
            SetCursorPosition(22, 13);
            nameRecipe = ReadLine();

            Functions.Clearing(0, 10, 70, 7);

            string prompt = "Add your ingredients here...";
            string[] options = { "Add", "Next" };
            Menu addButton = new Menu(prompt,options);

            bool next = false;

            do
            {
                int option = addButton.Run();
                switch(option)
                {
                    case 0:
                        singleFrame("Ingredients");
                        AddIngredient();
                         break;
                    case 1:
                        next = true;
                        break;
                }
            } while (!next);

            Functions.Clearing(0, 8, 70, 20);
            singleFrame("Preparing");
            Console.WriteLine("Write the way of preparing and press F2 when you finished editing: ".Pastel(Functions.yellowColor));
            SetCursorPosition(3, 13);
            EditPreparing();

            WriteRecipeToFile(Functions.default_pathfile);
        }

        /* -- File -- */

        public void ReadRecipeFromFile(string path)
        {
            /* STRUKTURA PLIKU
             * nazwa plik - nazwa przepisu
             * liczba - oznaczajaca liczbe skladnikow
             * lista składników
             * sposób przygotowania */
            StreamReader file = new StreamReader(path);
            nameRecipe = path.Replace(Functions.default_pathfile, "");
            nameRecipe = nameRecipe.Replace(".txt", "");
            int number_of_ingredients = -1;
            int temp = -1;
            string line;

            while((line=file.ReadLine())!=null)
            {
                if (number_of_ingredients == -1)
                {
                    /* -- pierwsza linia pliku -- */
                    number_of_ingredients = Convert.ToInt32(line);
                    temp = number_of_ingredients;
                }
                else if (temp>0)
                {
                    /* -- wczytywanie do pliku kolejnych skladnikow -- */
                    ingredients.Add(line);
                    temp--;
                }
                else if(temp == 0)
                {
                    /* -- wczytano wszystkie skladniki, wczytywanie sposobu przygowtowania -- */
                    preparing += line;
                }
            }
            file.Close();
        }

        public void WriteRecipeToFile(string path)
        {
            path += nameRecipe + ".txt";

            StreamWriter file = new StreamWriter(path);

            file.WriteLine(ingredients.Count);
            for (int i = 0; i < ingredients.Count; i++)
            {
                file.WriteLine(ingredients[i]);
            }

            file.WriteLine(preparing);

            file.Close();
        }

        /* -- Ingredients -- */

        private void ShowIngredients()
        {
            int k = 0;
            foreach (var item in ingredients)
            {
                SetCursorPosition(xIngFramePosition + 2, yIngFramePosition + 2+k);
                WriteLine(item.Pastel(Functions.nudeColor));
                k++;
            }
        }

        private void AddIngredient()
        {
            Console.WriteLine("Write an ingredient and press ENTER: ".Pastel(Functions.yellowColor));
            SetCursorPosition(3, 13);
            string ingredient = ReadLine();
            //int index = ingredients.Count;
            ingredients.Add(ingredient);
        }

        private void EditIngredient(int index)
        {
            SetCursorPosition(3, 12);
            Console.WriteLine("Edit the ingredient and press F2 when you finished editing: ".Pastel(Functions.yellowColor));
            SetCursorPosition(3, 13);
            string temp = Functions.ReadLine(ingredients[index]);
            //ingredients[index] = Functions.ReadLine(ingredients[index]);
            ingredients.RemoveAt(index);
            ingredients.Insert(index, temp);
        }

        private void RemoveIngredient(int index)
        {
            ingredients.RemoveAt(index);
        }

        private void OptionsMenu()
        {
            Functions.DrawFrame(1, 10, 70, 20, "Ingredients");
            SetCursorPosition(3, 12);

            string[] options = { "Add", "Edit", "Delete" };
            Menu optionsMenu = new Menu("Chose an option...", options);
            int optionIndex = optionsMenu.Run();

            Menu ingredientsMenu;
            int ingredientIndex;

            switch (optionIndex)
            {
                case 0:
                    singleFrame("Ingredients");
                    AddIngredient();
                    break;
                case 1:
                    ingredientsMenu = new Menu("Select the ingredient which you want to modify", ingredients.ToArray());
                    ingredientIndex = ingredientsMenu.Run();
                    singleFrame("Ingredients");
                    EditIngredient(ingredientIndex);
                    break;
                case 2:
                    ingredientsMenu = new Menu("Select the ingredient which you want to delete", ingredients.ToArray());
                    ingredientIndex = ingredientsMenu.Run();
                    singleFrame("Ingredients");
                    RemoveIngredient(ingredientIndex);
                    break;
            }
        }

        /* -- Preparing -- */

        private void ShowPreparing()
        {
            preparing = preparing.Pastel(Functions.nudeColor);

            int length = preparing.Length;
            for (int i = 0, k=0; i < length; i+=50, k++)
            {
                SetCursorPosition(xPreFramePosition + 2, yPreFramePosition + 2+k);
                if (length-i>=50)
                    WriteLine(preparing.Substring(i, 50));
                else
                    WriteLine(preparing.Substring(i, length - i));
            }                      
        }

        private void EditPreparing()
        {
            /*Functions.Clearing(2, 11, 69, 19);
            SetCursorPosition(3, 12);*/
            singleFrame("Preparing");
            Console.WriteLine("Write the way of preparing and press F2 when you finished editing: ".Pastel(Functions.yellowColor));
            SetCursorPosition(3, 13);
            preparing = Functions.ReadLine(preparing);
        }


        private void singleFrame(string title)
        {
            Functions.Clearing(2, 11, 69, 19);
            Functions.DrawFrame(1, 10, 70, 20, title);
            SetCursorPosition(3, 12);
        }
    }
}
