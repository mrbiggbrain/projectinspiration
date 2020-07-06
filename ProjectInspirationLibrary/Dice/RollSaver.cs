//-----------------------------------------------------------------------
// <copyright file="RollSaver.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class used to save rolls to be recalled later.
    /// </summary>
    public sealed class RollSaver
    {
        /// <summary>
        /// Holds a list of previous rolls for players.
        /// </summary>
        private readonly Dictionary<string, List<List<RollResult>>> prevRolls = new Dictionary<string, List<List<RollResult>>>();

        /// <summary>
        /// Gets a singleton instance for the class. 
        /// </summary>
        public static RollSaver Instance { get; } = new RollSaver();

        /// <summary>
        /// Saves a roll to the in-memory array. 
        /// </summary>
        /// <param name="name">Name to use for lookup later..</param>
        /// <param name="rolls">The results to save.</param>
        public void Save(string name, List<List<RollResult>> rolls)
        {
            this.prevRolls[name] = rolls;
        }

        /// <summary>
        /// Loads a roll from the in-memory array.
        /// </summary>
        /// <param name="name">Name to use for lookup.</param>
        /// <returns>The loaded results.</returns>
        public List<List<RollResult>> Load(string name)
        {
            this.prevRolls.TryGetValue(name, out List<List<RollResult>> results);

            return results;
        }
    }
}
