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
            IDataGetter dg = new FileSystemDataGetter();

            var pizzas = dg.getPizzas();
            var teams = dg.getTeams();


            // TODO: Yo kan vi flytte dette til en implementation?
            /*pizzas.Sort(delegate (Pizza x, Pizza y)
            {
                if (x.ingredients.Length > y.ingredients.Length) return 1;
                else
                {
                    return -1;
                }
            });*/


            //Udregner udr = new UdregnerImpl(pizzas.ToArray(), teams);
            Udregner udr = new LucasImpl(pizzas.ToArray(), teams);

            var del = udr.getBestDeliveryUsingIndex();

            int score = 0;

            foreach (var d in del)
            {
               score += d.CalculateScore(pizzas.ToArray());
            }

            Console.WriteLine(score);

            SolutionExporter.ExportSolution(del);

        }
    }
}
