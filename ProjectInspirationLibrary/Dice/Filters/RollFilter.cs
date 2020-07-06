//-----------------------------------------------------------------------
// <copyright file="RollFilter.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides methods for filtering the results of rolls. 
    /// </summary>
    public static class RollFilter
    {
        /// <summary>
        /// Apply disadvantage to the set of rolls in place. 
        /// </summary>
        /// <param name="rolls">The rolls to apply disadvantage to.</param>
        public static void Disadvantage(List<RollResult> rolls)
        {
            var validCount = rolls.Count() / 2;
            var firstRoll = rolls.Take(validCount);
            var secondRoll = rolls.Skip(validCount).Take(validCount);
            var keepRoll = firstRoll.Sum(x => x.Result) <= secondRoll.Sum(x => x.Result) ? firstRoll : secondRoll;

            foreach (var item in keepRoll)
            {
                item.Valid = true;
            }
        }

        /// <summary>
        /// Apply advantage to the set of rolls in place. 
        /// </summary>
        /// <param name="rolls">The rolls to apply advantage to.</param>
        public static void Advantage(List<RollResult> rolls)
        {
            var validCount = rolls.Count() / 2;
            var firstRoll = rolls.Take(validCount);
            var secondRoll = rolls.Skip(validCount).Take(validCount);
            var keepRoll = firstRoll.Sum(x => x.Result) >= secondRoll.Sum(x => x.Result) ? firstRoll : secondRoll;

            foreach (var item in keepRoll)
            {
                item.Valid = true;
            }
        }

        /// <summary>
        /// Keeps the lowest X rolls from the set.
        /// </summary>
        /// <param name="rolls">The rolls to apply the filter to.</param>
        /// <param name="filterValue">The number of rolls to keep.</param>
        public static void KeepLow(List<RollResult> rolls, int filterValue)
        {
            var validItems = (from result in rolls orderby result.Result ascending select result).Take(filterValue);

            foreach (var item in validItems)
            {
                item.Valid = true;
            }
        }

        /// <summary>
        /// Keeps the highest X rolls from the set.
        /// </summary>
        /// <param name="rolls">The rolls to apply the filter to.</param>
        /// <param name="filterValue">The number of rolls to keep.</param>
        public static void KeepHigh(List<RollResult> rolls, int filterValue)
        {
            var validItems = (from result in rolls orderby result.Result descending select result).Take(filterValue);

            foreach (var item in validItems)
            {
                item.Valid = true;
            }
        }
    }
}
