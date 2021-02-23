using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Udregnere
{
    public  class UdregnerImpl : Udregner
    {

        public UdregnerImpl(Pizza[] pizzas, int[] teams) : base(pizzas, teams)
        {

        }

        public override List<Delivery> getBestDelivery()
        {
            
            
            
            int[] newTeams = new int[teams.Length];
            teams.CopyTo(newTeams,0);




            List<List<Pizza>> pizzaStacks = new List<List<Pizza>>();
            pizzaStacks.Add(new List<Pizza>());


            //for (int i = teams.Length-1; i >= 0; i--)
            //{
            //    for (int j = 0; j < teams[i]; j++)
            //    {
            //        pizzaStacks.Add(new List<Pizza>(i+2));
            //    }
            //}


            foreach (Pizza p in pizzas)
            {
                bool PizzaPlaced = false;

                foreach (var stack in pizzaStacks)
                {
                    bool inStack = false;

                    foreach (string ingredience in p.ingredients)
                    {
                        if (stack.Exists(x => x.ingredients.ToList().Contains(ingredience)))
                        {
                            inStack = true;
                            break;
                        }
                    }

                    if (!inStack)
                    {
                        stack.Add(p);
                        PizzaPlaced = true;
                        break;
                    }
                }

                if (!PizzaPlaced)
                {
                    pizzaStacks.Add(new List<Pizza>());
                    pizzaStacks[pizzaStacks.Count-1].Add(p);
                }
            }



            List<Delivery> returDeliveries = new List<Delivery>();

            foreach (var v in pizzaStacks)
            {
                returDeliveries.Add(new Delivery(v.Count));
                returDeliveries[returDeliveries.Count - 1].Pizzas = v;
            }

            return returDeliveries;

        }
    }
}