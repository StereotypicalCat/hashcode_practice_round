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

                if (rarities.Count >= 2)
                {
                    candidateAnswers[0] = CreateDeliveryWithSizeOf(2, rarities);
                }

                if (rarities.Count >= 3)
                {
                    candidateAnswers[1] = CreateDeliveryWithSizeOf(3, rarities);
                }

                if (rarities.Count >= 4)
                {
                    candidateAnswers[2] = CreateDeliveryWithSizeOf(4, rarities);
                }
                // Select the one with the highest new score.
                var scores = candidateAnswers.Where(x => x != null).Select(x => x.candidateDelivery.CalculateScore(pizzas)/x.candidateDelivery.Pizzas.Count).ToArray();
                var bestIndex = Array.IndexOf(scores, scores.Max());

                rarities = candidateAnswers[bestIndex].Rarities;
                deliveries.Add(candidateAnswers[bestIndex].candidateDelivery);

                rarities = dirtyFix(rarities);

                if (rarities.Count < 2)
                {
                    canContinue = false;
                }
                
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

                var bestPizzas = RaritiesToBestScoreDictionary(rarities,
                    candidateDelivery.Pizzas.Select(p => pizzas[p].ingredients).SelectMany(x => x).ToArray());

                var indexOfHighestValue = bestPizzas.Keys.Max();
                
                candidateDelivery.AddPizza(bestPizzas[indexOfHighestValue]);

                foreach (var rarity in rarities)
                {
                    if (rarity.Value.Contains(bestPizzas[indexOfHighestValue]))
                    {
                        rarity.Value.Remove(bestPizzas[indexOfHighestValue]);
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

        private SortedDictionary<float, int> RaritiesToBestScoreDictionary(SortedDictionary<float, List<int>> rarities, string[] ingredients)
        {
            
            SortedDictionary<float, int> scoreIncreases = new SortedDictionary<float, int>();

            rarities = dirtyFix(rarities);
            
            foreach (var entry in rarities)
            {

                var newIngredients = pizzas[entry.Value[0]].ingredients;

                var totalNewIngredientsAdded =
                    newIngredients.Concat(ingredients).GroupBy(x => x).Count(g => Enumerable.Count<string>(g) > 1);

                var newScore = totalNewIngredientsAdded / entry.Key;

                scoreIncreases[newScore] = entry.Value[0];
                // Get new total ingredients that would be added to pizza
            }

            return scoreIncreases;

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