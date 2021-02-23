using System.Collections.Generic;

namespace ConsoleApp1.DataStrategy
{
    public interface IDataGetter
    {
        public List<Pizza> getPizzas();
        public int[] getTeams();
    }
}