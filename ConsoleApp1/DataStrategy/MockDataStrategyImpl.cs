using System.Collections.Generic;

namespace ConsoleApp1.DataStrategy
{
    public class MockDataStrategyImpl : IDataGetter
    {
        public List<Pizza> getPizzas()
        {
            var pizzas = new List<Pizza>
            {
                new Pizza(new []{"onion", "pepper", "olive"}),
                new Pizza(new []{"mushroom", "tomato", "basil"}),
                new Pizza(new []{"chicken", "mushroom", "pepper"}),
                new Pizza(new []{"tomato", "mushroom", "basil"}),
                new Pizza(new []{"chicken", "basil"})
            };
            return pizzas;
        }

        public int[] getTeams()
        {
            var teams = new int[]
            {
                1, 2, 1
            };
            return teams;
        }
    }
}