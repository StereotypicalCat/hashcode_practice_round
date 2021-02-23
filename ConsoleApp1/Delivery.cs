using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Delivery
    {
        public Delivery(int teamSize)
        {
            this.teamSize = teamSize;
            pizzas = new List<Pizza>();
        }

        public int teamSize { get; set; }
        public List<Pizza> pizzas;


        public void AddPizza(Pizza newPizza)
        {
            pizzas.Add(newPizza);
        }

        public int calculateScore()
        {
            int score = 0;

            List<string> ingrediences = new List<string>();

            foreach (Pizza p in pizzas)
            {
                ingrediences.AddRange(p.ingredients);
            }

            score = (int) Math.Pow( ingrediences.Distinct().Count(),2);

            return score;
        }
    }
}