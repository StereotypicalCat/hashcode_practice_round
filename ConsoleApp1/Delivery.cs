using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Delivery
    {
        public Delivery(int teamSize)
        {
            this.TeamSize = teamSize;
            Pizzas = new List<Pizza>();
        }

        public int TeamSize { get; set; }
        public List<Pizza> Pizzas;
        
        public void AddPizza(Pizza newPizza)
        {
            Pizzas.Add(newPizza);
        }

        public int CalculateScore()
        {
            int score = 0;

            List<string> ingredients = new List<string>();

            foreach (Pizza p in Pizzas)
            {
                ingredients.AddRange(p.ingredients);
            }

            score = (int) Math.Pow( ingredients.Distinct().Count(),2);

            return score;
        }
    }
}