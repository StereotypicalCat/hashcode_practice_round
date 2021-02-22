﻿using System.Collections.Generic;
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



    public class UdregnerBenjaImpl : Udregner
    {

        public UdregnerBenjaImpl(Pizza[] pizzas, int[] teams) : base(pizzas, teams)
        {

        }



        public override List<Delivery> Algoritm()
        {
            
        }


        private List<Delivery> CalculateteamSize()
        {
            int PizzaNum = pizzas.Length;

            List<List<Delivery>> returnDelivary = new List<List<Delivery>>();
            returnDelivary.Add(new List<Delivery>());


                int[] tempTeams = new int[teams.Length];
                teams.CopyTo(tempTeams, 0);

                for (int i = 0; i < teams.Length; i++)
                {
                    tempTeams[i]--;
                }

            return returnDelivary;
        }


    }

    








    public  class UdregnerImpl : Udregner
    {

        public UdregnerImpl(Pizza[] pizzas, int[] teams) : base(pizzas, teams)
        {

        }

        public override List<Delivery> Algoritm()
        {
            List<List<Pizza>> pizzaStacks = new List<List<Pizza>>();
            pizzaStacks.Add(new List<Pizza>());


            foreach (Pizza p in pizzas)
            {
                foreach (var stack in pizzaStacks)
                {
                    foreach (string ingredience in p.ingredients)
                    {
                        if (stack.Exists(x => ))
                        {
                        }
                    }
                    
                }

                
                
            }
        }
    }
}