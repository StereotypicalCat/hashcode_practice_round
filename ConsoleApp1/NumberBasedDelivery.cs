using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class NumberBasedDelivery
    {
        public NumberBasedDelivery(int teamSize)
        {
            this.TeamSize = teamSize;
            Pizzas = new List<int>();
        }

        public int TeamSize { get; set; }
        public List<int> Pizzas;
        
        public void AddPizza(int newPizzaIndex)
        {
            Pizzas.Add(newPizzaIndex);
        }

        public int CalculateScore(List<Pizza> pizzaList)
        {
            int score = 0;

            List<string> ingredients = new List<string>();

            foreach (int p in Pizzas)
            {
                var pizza = pizzaList[p];
                ingredients.AddRange(pizza.ingredients);
            }

            score = (int) Math.Pow( ingredients.Distinct().Count(),2);

            return score;
        }
    }
}