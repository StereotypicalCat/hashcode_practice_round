using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    public abstract class Udregner
    {
        public List<Delivery> deliveries;

        public Pizza[] pizzas;
        public int[] teams;


        public Udregner()
        {

        }

        protected Udregner(Pizza[] pizzas, int[] teams)
        {
            this.pizzas = pizzas;
            this.teams = teams;
            deliveries = new List<Delivery>();
        }

        public abstract List<Delivery> Algoritm();
    }



    //public class UdregnerBenjaImpl : Udregner
    //{

    //    public UdregnerBenjaImpl(Pizza[] pizzas, int[] teams) : base(pizzas, teams)
    //    {

    //    }



    //    public override List<Delivery> Algoritm()
    //    {
            
    //    }


    //    private List<Delivery> CalculateteamSize()
    //    {
    //        int PizzaNum = pizzas.Length;

    //        List<List<Delivery>> returnDelivary = new List<List<Delivery>>();
    //        returnDelivary.Add(new List<Delivery>());


    //            int[] tempTeams = new int[teams.Length];
    //            teams.CopyTo(tempTeams, 0);

    //            for (int i = 0; i < teams.Length; i++)
    //            {
    //                tempTeams[i]--;
    //            }

    //        return returnDelivary;
    //    }
    //}

    








    public  class UdregnerImpl : Udregner
    {






        public UdregnerImpl(Pizza[] pizzas, int[] teams) : base(pizzas, teams)
        {

        }

        public override List<Delivery> Algoritm()
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
                returDeliveries[returDeliveries.Count - 1].pizzas = v;
            }

            return returDeliveries;

        }
    }
}