using System;
using System.Threading.Tasks.Sources;
using ConsoleApp1;
using ConsoleApp1.DataStrategy;
using ConsoleApp1.Udregnere;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataGetter dg = new MockDataStrategyImpl();

            var pizzas = dg.getPizzas();
            var teams = dg.getTeams();


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
}
