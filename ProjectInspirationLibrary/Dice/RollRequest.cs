// <copyright file="RollRequest.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProjectInspirationLibrary.Dice.Filters;
    using ProjectInspirationLibrary.Dice.Parser;

    /// <summary>
    /// Represents a request to roll a set of dice.
    /// </summary>
    public class RollRequest
    {
        /// <summary>
        /// Stores the number of dice to roll.
        /// </summary>
        private readonly int count;

        /// <summary>
        /// Stores the number of sides each die has.
        /// </summary>
        private readonly int sides;

        /// <summary>
        /// Stores the type of filter used for the results.
        /// </summary>
        private readonly FilterType filterType;

        /// <summary>
        /// Stores the value the filter uses to filter the results.
        /// </summary>
        private readonly int filterValue;

        /// <summary>
        /// Stores a value determining if the results add or subtract from the total results of a roll.
        /// </summary>
        private int negative = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="RollRequest" /> class.
        /// </summary>
        /// <param name="count">Number of dice to roll.</param>
        /// <param name="sides">Number of sides each die has.</param>
        /// <param name="filterType">The type of filter to apply to results.</param>
        /// <param name="filterValue">Value the filter uses to filter results.</param>
        public RollRequest(int count, int sides, FilterType filterType, int filterValue)
        {
            this.count = count;
            this.sides = sides;
            this.filterType = filterType;
            this.filterValue = filterValue;
        }

        /// <summary>
        /// Sets the request to be a subtractive roll.
        /// </summary>
        /// <returns>A reference to the object being manipulated.</returns>
        public RollRequest IsNeg()
        {
            this.negative = -1;
            return this;
        }

        /// <summary>
        /// Rolls the request and returns the results.
        /// </summary>
        /// <returns>A <see cref="List{RollResult}" /> containing the results of the roll."</returns>
        public List<RollResult> Roll()
        {
            // Perform actual dice roll. 
            List<RollResult> results = RollRequest.Roll(this.count, this.sides, this.negative);

            // Apply filters in-place
            RollRequest.ApplyFilters(results, this.filterType, this.filterValue);

            return results;
        }

        /// <summary>
        /// Static helper function that performs the given roll.
        /// </summary>
        /// <param name="count">Number of dice to roll.</param>
        /// <param name="sides">Number of sides for each die.</param>
        /// <param name="negative">Should the results be negative.</param>
        /// <returns>A <see cref="List{RollResult}" /> containing the results of the roll.</returns>
        private static List<RollResult> Roll(int count, int sides, int negative)
        {
            List<RollResult> results = new List<RollResult>();
            Random random = new Random();

            if (count == 0)
            {
                RollResult r = new RollResult(sides * negative, sides)
                {
                    Valid = true
                };
                results.Add(r);
            }

            foreach (int i in Enumerable.Range(1, count))
            {
                int roll = (random.Next(sides) + 1) * negative;

                results.Add(new RollResult(roll, sides));
            }

            return results;
        }

        /// <summary>
        /// Applies the correct filter to the results in place.
        /// </summary>
        /// <param name="rolls">A <see cref="List{RollResult}" /> to be filtered.</param>
        /// <param name="filterType">The type of filter to be applied.</param>
        /// <param name="filterValue">The parameter for the filter.</param>
        private static void ApplyFilters(List<RollResult> rolls, FilterType filterType, int filterValue)
        {
            if (filterType == FilterType.KEEPHIGH)
            {
                RollFilter.KeepHigh(rolls, filterValue);
            }
            else if (filterType == FilterType.KEEPLOW)
            {
                RollFilter.KeepLow(rolls, filterValue);
            }
            else if (filterType == FilterType.ADVANTAGE)
            {
                RollFilter.Advantage(rolls);
            }
            else if (filterType == FilterType.DISADVANTAGE)
            {
                RollFilter.Disadvantage(rolls);
            }
        } 
    }
}