using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Udregnere
{
    public class LucasImpl : Udregner
    {
        
        public LucasImpl(Pizza[] pizzas, int[] teams) : base(pizzas, teams)
        {

        }
        
        public override List<NumberBasedDelivery> getBestDeliveryUsingIndex()
        {
            
            var rarities = getPizzaRarities(pizzas);
            var deliveries = new List<NumberBasedDelivery>();

            bool canContinue = true;

            while (canContinue)
            {
                // Create 3 test deliveries of size 1, 2 and 3.
                // Select least rarest pizza.

                var candidateAnswers = new candidateBestAnswer[3];

                if (rarities.Count >= 2  && teams[0] > 0)
                {
                    candidateAnswers[0] = CreateDeliveryWithSizeOf(2, rarities);
                }

                if (rarities.Count >= 3 && teams[1] > 0)
                {
                    candidateAnswers[1] = CreateDeliveryWithSizeOf(3, rarities);
                }

                if (rarities.Count >= 4 && teams[2] > 0)
                {
                    candidateAnswers[2] = CreateDeliveryWithSizeOf(4, rarities);
                }

                var scores = new Nullable<float>[3];

                var skew = 0.5f;
                
                scores[0] = candidateAnswers[0]?.candidateDelivery.CalculateScore(pizzas)/(Math.Max(2 - skew, 1));
                scores[1] = candidateAnswers[1]?.candidateDelivery.CalculateScore(pizzas)/(Math.Max(3 - skew, 1));
                scores[2] = candidateAnswers[2]?.candidateDelivery.CalculateScore(pizzas)/(Math.Max(4 - skew, 1));

                var bestIndex = Nullable.Compare(scores[0], scores[1]) > 0 ? (Nullable.Compare(scores[0], scores[2]) > 0 ? 0 : 2) : (Nullable.Compare(scores[1], scores[2]) > 0 ? 1 : 2);
                
                
                teams[bestIndex]--;
                
                rarities = candidateAnswers[bestIndex].Rarities;
                deliveries.Add(candidateAnswers[bestIndex].candidateDelivery);

                rarities = dirtyFix(rarities);

                if (rarities.Count < 2)
                {
                    canContinue = false;
                }

                Console.WriteLine(((1 - (((float) rarities.Count) / ((float) pizzas.Length))))*100 + "% done");
            }


            return deliveries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pizzas"></param>
        /// <returns>A dictionary. Pizza at i has rarity returned form dictionary</returns>
        private SortedDictionary<float, List<int>> getPizzaRarities(Pizza[] pizzas)
        {
            var noOfEachIngredient = new Dictionary<String, int>();

            foreach (var pizza in pizzas)
            {
                foreach (var ingredient in pizza.ingredients)
                {
                    if (noOfEachIngredient.ContainsKey(ingredient))
                    {
                        noOfEachIngredient[ingredient] = noOfEachIngredient[ingredient] + 1;
                    }
                    else
                    {
                        noOfEachIngredient.Add(ingredient, 1);
                    }
                    
                }
            }

            var ingredientRarities = new Dictionary<String, float>();

            foreach (var entry in noOfEachIngredient)
            {
                ingredientRarities[entry.Key] = (1.0f / entry.Value);
            }

            var pizzaRarities = new float[pizzas.Length];

            for (int i = 0; i < pizzas.Length; i++)
            {
                var pizza = pizzas[i];

                var score = 0f;
                foreach (var ingredient in pizza.ingredients)
                {
                    score += ingredientRarities[ingredient];
                }

                pizzaRarities[i] = score;
            }

            var rarityDictionary = new SortedDictionary<float, List<int>>();

            for (int i = 0; i < pizzaRarities.Length; i++)
            {
                if (rarityDictionary.ContainsKey(pizzaRarities[i]))
                {
                    rarityDictionary[pizzaRarities[i]].Add(i);
                }
                else
                {
                    rarityDictionary[pizzaRarities[i]] = new List<int> {i};
                }
            }
            
            return rarityDictionary;
        }

        private float GetRarityOfPizzaWithLeastRarity(SortedDictionary<float, List<int>> pizzaRarities)
        {
            var keys = pizzaRarities.Keys.ToList();


            return keys[0];
        }

        private candidateBestAnswer CreateDeliveryWithSizeOf(int size, SortedDictionary<float, List<int>> orignialRarities)
        {
            var rarities = new SortedDictionary<float, List<int>>();
            // Copy array
            foreach (var rarity in orignialRarities)
            {
                var copyOfList = new List<int>(rarity.Value);
                rarities.Add(rarity.Key, copyOfList);
            }
            
            NumberBasedDelivery candidateDelivery = new NumberBasedDelivery(size);
            // Start of with least rarest pizza.
            var leastRarestPizzaRarity = GetRarityOfPizzaWithLeastRarity(rarities);

            var offset = pizzas.Length - orignialRarities.Values.SelectMany(t => t).Count();

            candidateDelivery.AddPizza(rarities[leastRarestPizzaRarity][0]);
            rarities[leastRarestPizzaRarity].RemoveAt(0);
            if (rarities[leastRarestPizzaRarity].Count == 0)
            {
                rarities.Remove(leastRarestPizzaRarity);
            }
            // Find least rarest pizza which shares no ingredients.
            for (int i = 1; i < size; i++)
            {

                var bestPizzaIndex = RaritiesToBestScoreDictionary(rarities,
                    candidateDelivery.Pizzas.Select(p => pizzas[p].ingredients).SelectMany(x => x).ToArray());

                candidateDelivery.AddPizza(bestPizzaIndex);

                foreach (var rarity in rarities)
                {
                    if (rarity.Value.Contains(bestPizzaIndex))
                    {
                        rarity.Value.Remove(bestPizzaIndex);
                        break;
                    }

                    if (rarity.Value.Count == 0)
                    {
                        rarities.Remove(rarity.Key);
                    }
                }
            }

            candidateBestAnswer candidateBestAnswer = new candidateBestAnswer();
            candidateBestAnswer.candidateDelivery = candidateDelivery;
            candidateBestAnswer.Rarities = rarities;

            return candidateBestAnswer;
        }

        private int RaritiesToBestScoreDictionary(SortedDictionary<float, List<int>> rarities, string[] ingredients)
        {

            int indexOfBestNewPizza = -1;
            float bestScore = float.MinValue;

            rarities = dirtyFix(rarities);
            
            foreach (var entry in rarities)
            {

                var newIngredients = pizzas[entry.Value[0]].ingredients;

                int totalNewIngredientsAdded = 0;
                foreach (var ingredient in newIngredients)
                {
                    if (!ingredients.Contains(ingredient))
                    {
                        totalNewIngredientsAdded++;
                    }
                }

                var newScore = totalNewIngredientsAdded / entry.Key;

                if (newScore > bestScore)
                {
                    bestScore = newScore;
                    indexOfBestNewPizza = entry.Value[0];
                }
            }

            return indexOfBestNewPizza;

        }

        private SortedDictionary<float, List<int>> dirtyFix(SortedDictionary<float,List<int>> rarities)
        {
            var keys = rarities.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                if (rarities[keys[i]].Count == 0)
                {
                    rarities.Remove(keys[i]);
                }
                
            }
            return rarities;
        }
    }

    internal class candidateBestAnswer
    {
        public SortedDictionary<float, List<int>> Rarities { get; set; }
        public NumberBasedDelivery candidateDelivery { get; set; }
    }
}