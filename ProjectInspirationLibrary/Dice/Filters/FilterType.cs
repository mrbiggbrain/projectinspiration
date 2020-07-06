//-----------------------------------------------------------------------
// <copyright file="FilterType.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Denotes the type of filter that should be applied to the roll. 
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// X high rolls should be kept. 
        /// </summary>
        KEEP_HIGH,

        /// <summary>
        /// X low rolls should be kept. 
        /// </summary>
        KEEP_LOW,

        /// <summary>
        /// Roll twice as many dice and keep high. 
        /// </summary>
        ADVANTAGE,

        /// <summary>
        /// Roll twice as many dice and keep low. 
        /// </summary>
        DISADVANTAGE,

        /// <summary>
        /// No special rules. 
        /// </summary>
        NONE
    }
}
