using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    
    public static class SolutionExporter
    {
        
        
        // TODO: Reimplement export.
        /// <summary>
        /// Exports the solution as solution.txt looking up the pizza of each delivery.
        /// </summary>
        /// <param name="solution"></param>
        public static void ExportSolution(List<Delivery> solution)
        {

        }
        /// <summary>
        /// Exportd the solution as solution.txt using already gotten pizza numbers.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="teamsSizes"></param>
        public static void ExportSolution(List<NumberBasedDelivery> solution)
        {
            List<String> answerLines = new List<string>();
            
            answerLines.Add(solution.Count.ToString());

            foreach (var delivery in solution)
            {
                string line = delivery.TeamSize.ToString();
                foreach (var index in delivery.Pizzas)
                {
                    line += " " + index;
                }
                answerLines.Add(line);
            }

            var outputPath = Directory.GetCurrentDirectory() + "\\answer.txt";

            Console.WriteLine("writing file to...:");
            Console.WriteLine(outputPath);
            
            File.WriteAllLines(outputPath, answerLines);
        }
        
        
    }
}