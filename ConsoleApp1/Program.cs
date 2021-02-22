using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;

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

            var pizzas = new List<Pizza>
            {
                new Pizza(new []{"onion", "pepper", "olive"}),
                new Pizza(new []{"mushroom", "tomato", "basil"}),
                new Pizza(new []{"chicken", "mushroom", "pepper"}),
                new Pizza(new []{"tomato", "mushroom", "basil"}),
                new Pizza(new []{"chicken", "basil"})
            };


            pizzas.Sort(delegate (Pizza x, Pizza y)
            {
                if (x.ingredients.Length > y.ingredients.Length) return 1;
                else
                {
                    return -1;
                }
            });


            Udregner Udr = new UdregnerImpl(pizzas.ToArray(), teams);

            var del = Udr.Algoritm();

            int score = 0;

            foreach (var d in del)
            {
               score += d.calculateScore();
            }

            Console.WriteLine(score);
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

            score = (int) Math.Pow( ingrediences.Distinct().Count(),2);

            return score;
        }
    }
}
