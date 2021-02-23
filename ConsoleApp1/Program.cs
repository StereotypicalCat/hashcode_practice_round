using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    class Program
    {

        public static int bestScore;
        public static List<Delivery> bestDelivery;
        public static List<Pizza> loadedPizzas;


        static void Main(string[] args)
        {
            var teams = new int[3];
            List<Pizza> pizzas = new List<Pizza>();

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\lucas\RiderProjects\practice_round\ConsoleApp1\input.txt");
            string[] startInfo = lines[0].Split(" ");
            teams[0] = Int32.Parse(startInfo[1]);
            teams[1] = Int32.Parse(startInfo[2]);
            teams[2] = Int32.Parse(startInfo[3]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] ingredientsInput = lines[i].Split(" ");

                string[] ingredients = new string[ingredientsInput.Length-1];
                for (int j = 1; j < ingredientsInput.Length; j++)
                {
                    ingredients[j - 1] = ingredientsInput[j];
                }
                pizzas.Add(new Pizza(ingredients));
            }


            var pizzasLeft = new List<int>();
            for(int i = 0; i < pizzas.Count; i++)
            {
                pizzasLeft.Add(i);
            }

            loadedPizzas = pizzas;

            Console.WriteLine("Begun finding solutions....");
            solve(teams, pizzasLeft, new List<Delivery>());

            Console.WriteLine("Found solutions, converting them...");
            List<String> answerLines = new List<string>();
            
            answerLines.Add(bestDelivery.Count.ToString());

            foreach (var delivery in bestDelivery)
            {
                string line = delivery.teamSize.ToString();
                foreach (var index in delivery.pizzaIndexes)
                {
                    line += " " + index;
                }
                answerLines.Add(line);
            }

            
            File.WriteAllLines(@"C:\Users\lucas\RiderProjects\practice_round\ConsoleApp1\answer.txt", answerLines);
        }

        static void solve(int[] teams, List<int> pizzas, List<Delivery> deliveries)
        {
            
            if (pizzas.Count == loadedPizzas.Count)
            {
                Console.WriteLine("Testing all 2-pizza start configurations");
            }
            
            int[] removedPizzas = new int[4];
            for (int i = 0; i < teams[0]; i++)
            {
                for (int j = 0; j < pizzas.Count; j++)
                {
                    for (int k = j+1; k < pizzas.Count; k++)
                    {
                        Delivery delivery = new Delivery(2);
                        removedPizzas[0] = pizzas[k];
                        removedPizzas[1] = pizzas[j];
                        delivery.AddPizza(pizzas[j]);
                        delivery.AddPizza(pizzas[k]);
                        pizzas.RemoveAt(k);
                        pizzas.RemoveAt(j);
                        deliveries.Add(delivery);
                        solve(new int[]{teams[0]-1, teams[1], teams[2]}, pizzas, deliveries);
                        pizzas.Insert(j, removedPizzas[0]);
                        pizzas.Insert(k, removedPizzas[1]);
                        deliveries.RemoveAt(deliveries.Count-1);
                    }
                }
            }

            if (pizzas.Count == loadedPizzas.Count)
            {
                Console.WriteLine("Testing all 3-pizza start configurations");
            }
            
            for (int i = 0; i < teams[1]; i++)
            {
                for (int j = 0; j < pizzas.Count; j++)
                {
                    for (int k = j+1; k < pizzas.Count; k++)
                    {
                        for (int l = k+1; l < pizzas.Count; l++)
                        {
                            Delivery delivery = new Delivery(3);
                            removedPizzas[0] = pizzas[j];
                            removedPizzas[1] = pizzas[k];
                            removedPizzas[2] = pizzas[l];
                            delivery.AddPizza(pizzas[l]);
                            delivery.AddPizza(pizzas[k]);
                            delivery.AddPizza(pizzas[j]);
                            pizzas.RemoveAt(l);
                            pizzas.RemoveAt(k);
                            pizzas.RemoveAt(j);
                            deliveries.Add(delivery);
                            solve(new int[]{teams[0], teams[1]-1, teams[2]}, pizzas, deliveries);
                            pizzas.Insert(j, removedPizzas[0]);
                            pizzas.Insert(k, removedPizzas[1]);
                            pizzas.Insert(l, removedPizzas[2]);
                            deliveries.RemoveAt(deliveries.Count-1);
                        }
                    }
                }
            }
            
            if (pizzas.Count == loadedPizzas.Count)
            {
                Console.WriteLine("Testing all 4-pizza start configurations");
            }
            
            for (int i = 0; i < teams[2]; i++)
            {
                for (int j = 0; j < pizzas.Count; j++)
                {
                    for (int k = j+1; k < pizzas.Count; k++)
                    {
                        for (int l = k+1; l < pizzas.Count; l++)
                        {
                            for (int m = l+1; m < pizzas.Count; m++)
                            {
                                Delivery delivery = new Delivery(4);
                                removedPizzas[0] = pizzas[j];
                                removedPizzas[1] = pizzas[k];
                                removedPizzas[2] = pizzas[l];
                                removedPizzas[3] = pizzas[m];
                                delivery.AddPizza(pizzas[j]);
                                delivery.AddPizza(pizzas[k]);
                                delivery.AddPizza(pizzas[l]);
                                delivery.AddPizza(pizzas[m]);
                                pizzas.RemoveAt(m);
                                pizzas.RemoveAt(l);
                                pizzas.RemoveAt(k);
                                pizzas.RemoveAt(j);
                                deliveries.Add(delivery);
                                solve(new int[]{teams[0], teams[1], teams[2]-1}, pizzas, deliveries);
                                pizzas.Insert(j, removedPizzas[0]);
                                pizzas.Insert(k, removedPizzas[1]);
                                pizzas.Insert(l, removedPizzas[2]);
                                pizzas.Insert(m, removedPizzas[3]);
                                deliveries.RemoveAt(deliveries.Count-1);
                            }
                        }
                    }
                }
            }

            int score = 0;
            foreach (var delivery in deliveries)
            {
                score += delivery.calculateScore();
            }
            
            if (score > bestScore)
            {
                bestScore = score;
                bestDelivery = new List<Delivery>(deliveries);
            }

        }
        
        
        public class Delivery
        {
            public Delivery(int teamSize)
            {
                this.teamSize = teamSize;
                pizzaIndexes = new List<int>();
            }

            public int teamSize { get; set; }
            public List<int> pizzaIndexes;


            public void AddPizza(int index)
            {
                pizzaIndexes.Add(index);
            }

            public int calculateScore()
            {

                int score = pizzaIndexes.Select(p => loadedPizzas[p].ingredients).SelectMany(p => p).Distinct().Count();
            
                return score * score;
            }
        }
        
    }

}
