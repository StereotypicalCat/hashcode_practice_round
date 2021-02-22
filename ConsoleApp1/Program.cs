using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var teams = new int[]
            {
                1, 2, 1
            };

            var pizzas = new Pizza[]
            {
                new Pizza(new []{"onion", "pepper", "olive"}),
                new Pizza(new []{"mushroom", "tomato", "basil"}),
                new Pizza(new []{"chicken", "mushroom", "pepper"}),
                new Pizza(new []{"tomato", "mushroom", "basil"}),
                new Pizza(new []{"chicken", "basil"})
            };
            



        }
    }

    public class Udregner
    {
        private List<Delivery> deliveries;


        public Udregner()
        {
            deliveries = new List<Delivery>();
        }

        public List<Delivery> Algoritm()
        {
            deliveries.Add(new Delivery(3));



        }

    }

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

            score = ingrediences.Distinct().Count();

            return score;
        }
    }
}
