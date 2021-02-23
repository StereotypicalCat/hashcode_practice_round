using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.DataStrategy
{
    public class FileSystemDataGetter : IDataGetter
    {
        private List<Pizza> _pizzas;
        private int[] _teams;
        
        public FileSystemDataGetter()
        {
            var inputpath = Directory.GetCurrentDirectory() + "\\input.txt";
            
            string[] lines = System.IO.File.ReadAllLines(inputpath);

            _teams = new int[3];
            _pizzas = new List<Pizza>();
            
            string[] startInfo = lines[0].Split(" ");
            _teams[0] = Int32.Parse(startInfo[1]);
            _teams[1] = Int32.Parse(startInfo[2]);
            _teams[2] = Int32.Parse(startInfo[3]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] ingredientsInput = lines[i].Split(" ");

                string[] ingredients = new string[ingredientsInput.Length-1];
                for (int j = 1; j < ingredientsInput.Length; j++)
                {
                    ingredients[j - 1] = ingredientsInput[j];
                }
                _pizzas.Add(new Pizza(ingredients));
            }
        }
        
        public List<Pizza> getPizzas()
        {
            return _pizzas;
        }

        public int[] getTeams()
        {
            return _teams;
        }
    }
}