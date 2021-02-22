using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {

        public int bestScore;
        public int bestDelivery;
        
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

        static void solve(int[] teams, Pizza[] pizzas, Delivery[] deliveries)
        {
            for (int i = 0; i < teams[0]; i++)
            {
                for (int j = i; j < pizzas.Length; j++)
                {
                    for (int k = j; k < pizzas.Length; k++)
                    {
                        // Lav en løsning
                        
                        solve(teams, pizzas, deliveries);
                    }
                }
            }
            
            for (int i = 0; i < teams[1]; i++)
            {
                for (int j = 0; j < pizzas.Length; j++)
                {
                    for (int k = 0; k < pizzas.Length; k++)
                    {
                        for (int l = 0; l < pizzas.Length; l++)
                        {
                            // Lav en løsning
                            solve(teams, pizzas, deliveries);
                        }
                    }
                }
            }
            for (int i = 0; i < teams[2]; i++)
            {
                for (int j = 0; j < pizzas.Length; j++)
                {
                    for (int k = 0; k < pizzas.Length; k++)
                    {
                        for (int l = 0; l < pizzas.Length; l++)
                        {
                            for (int m = 0; m < pizzas.Length; m++)
                            {
                                // Lav en løsning
                                solve(teams, pizzas, deliveries);
                            }
                        }
                    }
                }
            }
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
