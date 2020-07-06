//-----------------------------------------------------------------------
// <copyright file="RollResult.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ProjectInspirationLibrary.Dice.Parser;

    /// <summary>
    /// Represents the results of a roll.
    /// </summary>
    public class RollResult
    {
        /// <summary>
        /// Holds the results of the roll.
        /// </summary>
        private int result;

        /// <summary>
        /// Holds the maximum value for a roll.
        /// </summary>
        private int max;

        /// <summary>
        /// Holds a flag for if the results are valid.
        /// </summary>
        private bool valid = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="RollResult" /> class.
        /// </summary>
        /// <param name="result">The results of the roll.</param>
        /// <param name="max">The maximum possible value for the roll.</param>
        public RollResult(int result, int max)
        {
            this.Result = result;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets the results of the roll.
        /// </summary>
        public int Result { get => this.result; set => this.result = value; }

        /// <summary>
        /// Gets or sets the maximum value possible for the roll.
        /// </summary>
        public int Max { get => this.max; set => this.max = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the roll is valid.
        /// </summary>
        public bool Valid { get => this.valid; set => this.valid = value; }

        /// <summary>
        /// Determines if the results are a critical.
        /// </summary>
        /// <returns>True for critical rolls, false otherwise.</returns>
        public bool IsCrit()
        {
            return this.Result == this.Max;
        }

        /// <summary>
        /// Determines if the results are a critical fail.
        /// </summary>
        /// <returns>True if a critical fail, false otherwise.</returns>
        public bool IsCritFail()
        {
            return this.Result == 1;
        }
    }
}
