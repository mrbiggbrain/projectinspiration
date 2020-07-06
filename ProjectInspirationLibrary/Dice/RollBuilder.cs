// <copyright file="RollBuilder.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ProjectInspirationLibrary.Dice.Filters;

    /// <summary>
    /// Provides a simple interface for constructing complex rolls.
    /// </summary>
    public class RollBuilder
    {
        /// <summary>
        /// List of current requests the builder is maintaining.
        /// </summary>
        private readonly List<RollRequest> requests = new List<RollRequest>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RollBuilder" /> class
        /// </summary>
        public RollBuilder()
        {
        }

        /// <summary>
        /// Add a request to the builder.
        /// </summary>
        /// <param name="sign">Determines if the set is reducing overall value of the roll.</param>
        /// <param name="count">Number of dice to roll.</param>
        /// <param name="sides">Number of sides the dice have.</param>
        /// <param name="filterType">Filter to apply to the results.</param>
        /// <param name="filterValue">Filter value to use for the filter.</param>
        /// <returns>A <see cref="RollRequest" /> containing the requested set of rolls.</returns>
        public RollRequest AddRequest(int sign, int count, int sides, FilterType filterType, int filterValue)
        {
            RollRequest request = new RollRequest(count, sides, filterType, filterValue);

            if (sign < 0)
            {
                request.IsNeg();
            }

            this.requests.Add(request);
            return request;
        }

        /// <summary>
        /// Rolls the dice contained in the builder and returns the results.
        /// </summary>
        /// <returns>A <see cref="List{List{RollResult}} containing the results of the rolls." /></returns>
        public List<List<RollResult>> Roll()
        {
            List<List<RollResult>> results = new List<List<RollResult>>();

            foreach (RollRequest request in this.requests)
            {
                results.Add(request.Roll());
            }

            return results;
        }
    }
}
