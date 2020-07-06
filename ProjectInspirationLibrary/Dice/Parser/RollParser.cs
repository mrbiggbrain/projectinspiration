//-----------------------------------------------------------------------
// <copyright file="RollParser.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace ProjectInspirationLibrary.Dice.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using ProjectInspirationLibrary.Dice.Filters;

    /// <summary>
    /// Static class for parsing text into RollBuilder objects. 
    /// </summary>
    public static class RollParser
    {
        /// <summary>
        /// Process a string into a RollBuilder.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>A {RollBuilder} containing the rolls represented by the string.</returns>
        public static RollBuilder Parse(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            // Prepare the string
            string preparedString = RollParser.Prepare(str);

            // Parse Out Roll Signs
            List<int> signTable = RollParser.GetSignTable(preparedString);

            // Parse out Roll Text
            List<string> rollTable = RollParser.GetRollTextTable(preparedString);

            // Parse and construct the builder. 
            return RollParser.ParseAndBuild(rollTable, signTable);
        }

        /// <summary>
        /// Returns the text for the roll or a default roll text.
        /// </summary>
        /// <param name="rollText">The text to check.</param>
        /// <returns>The roll text back if valid, or a default roll.</returns>
        public static string TextOrDefault(string rollText)
        {
            if (string.IsNullOrEmpty(rollText))
            {
                return "1d20";
            }

            return rollText;
        }

        /// <summary>
        /// Checks if the text represents a valid roll string.
        /// </summary>
        /// <param name="text">String to check for validity.</param>
        /// <returns>True on a valid string, false on an invalid string</returns>
        public static bool Check(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            // Regex for checking rolls text.
            string regex = @"^(?:(?:(?:\d+d\d+(?:\s(?:adv|dis|kh\d+|kl\d+))?|\d+)))(\s?\+\s?(?:(?:(?:\d+d\d+(?:\s(?:adv|dis|kh\d+|kl\d+))?|\d+))))*(?:\s?#.*)?$";
            return Regex.IsMatch(text.ToLower(CultureInfo.CurrentCulture), regex);
        }

        /// <summary>
        /// Constructs the {RollBuilder} from the list of rolls and signs.
        /// </summary>
        /// <param name="rollTable">A list of strings containing representations of requested rolls.</param>
        /// <param name="signTable">A list of integers representing if the roll was added or subtracted.</param>
        /// <returns>A {RollBuilder} containing the represented rolls.</returns>
        private static RollBuilder ParseAndBuild(List<string> rollTable, List<int> signTable)
        {
            RollBuilder builder = new RollBuilder();

            for (int i = 0; i < rollTable.Count; i++)
            {
                (string nakedRollText, FilterType filterType, int filterValue) = PaseFilter(rollTable[i]);

                (int count, int sides) = ParseRollText(nakedRollText);

                if (filterType == FilterType.ADVANTAGE)
                {
                    filterValue = count;
                    count *= 2;
                }
                else if (filterType == FilterType.DISADVANTAGE)
                {
                    filterValue = count;
                    count *= 2;
                }
                else if (filterType == FilterType.NONE)
                {
                    filterType = FilterType.KEEPHIGH;
                    filterValue = count;
                }

                RollRequest r = builder.AddRequest(signTable[i], count, sides, filterType, filterValue);

                if (signTable[i] == -1)
                {
                    r.IsNeg();
                }
            }

            return builder;
        }

        /// <summary>
        /// Perform preparations and data formatting for later steps.
        /// </summary>
        /// <param name="str">The string to prepare.</param>
        /// <returns>The prepared string.</returns>
        private static string Prepare(string str)
        {
            return str.Replace(" ", string.Empty, ignoreCase: false, culture: CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert a string to numerical representations for the number of dice and sides.
        /// </summary>
        /// <param name="v">The string representing the roll.</param>
        /// <returns>A tuple with two integers representing the number of dice and their sides.</returns>
        private static (int count, int sides) ParseRollText(string v)
        {
            string[] table = v.Split('d');

            int count = 0;
            int sides = 20;

            // Parse count
            if (table.Length > 0)
            {
                if (int.TryParse(table[0], out count))
                {
                    // parsed;
                }

                // Parse sides
                if (table.Length > 1)
                {
                    if (int.TryParse(table[1], out sides))
                    {
                        // parsed;
                    }
                }
                else
                {
                    sides = count;
                    count = 0;
                }
            }

            return (count, sides);
        }

        /// <summary>
        /// Parses the string to determine the filter applied and its parameters. 
        /// </summary>
        /// <param name="v">The string to parse.</param>
        /// <returns>A tuple containing the actual roll text, the type of filter, and it's parameter.</returns>
        private static (string nakedRollText, FilterType filterType, int filterValue) PaseFilter(string v)
        {
            if (v.Contains("kh", StringComparison.Ordinal))
            {
                throw new NotImplementedException();
            }
            else if (v.Contains("kl", StringComparison.Ordinal))
            {
                throw new NotImplementedException();
            }
            else if (v.Contains("adv", StringComparison.Ordinal))
            {
                return RollParser.Advantage(v);
            }
            else if (v.Contains("dis", StringComparison.Ordinal))
            {
                return RollParser.Disadvantage(v);
            }
            else
            {
                return (v, FilterType.NONE, 0);
            }
        }

        /// <summary>
        /// Parses details for advantage.
        /// </summary>
        /// <param name="v">String to parse.</param>
        /// <returns> Tuple containing the actual roll text, the type of filter, and it's parameter.</returns>
        private static (string nakedRollText, FilterType filterType, int filterValue) Disadvantage(string v)
        {
            (string text, int _) = RollParser.ParseFilter(v, "dis");

            return (text, FilterType.DISADVANTAGE, 0);
        }

        /// <summary>
        /// Parse details for disadvantage.
        /// </summary>
        /// <param name="v">String to parse.</param>
        /// <returns> tuple containing the actual roll text, the type of filter, and it's parameter.</returns>
        private static (string nakedRollText, FilterType filterType, int filterValue) Advantage(string v)
        {
            (string text, int _) = RollParser.ParseFilter(v, "adv");

            return (text, FilterType.ADVANTAGE, 0);
        }

        /// <summary>
        /// Helper function to parse the left and right sides of given filter text.
        /// </summary>
        /// <param name="v">String to parse.</param>
        /// <param name="t">Filter text to parse the left and right of.</param>
        /// <returns>A tuple containing the text to the left and count to the right of given filter.</returns>
        private static (string text, int count) ParseFilter(string v, string t)
        {
            // Split the parts
            var parts = v.Split(t);

            string text = string.Empty;
            int count = 0;

            if (parts.Length > 0)
            {
                text = parts.ElementAt(0);

                if (parts.Length > 1)
                {
                    if (int.TryParse(parts.ElementAt(1), out count))
                    {
                        // parsed;
                    }
                }
            }

            return (text, count);
        }

        /// <summary>
        /// Splits the text on operator boundaries.
        /// </summary>
        /// <param name="str">The string to break into rolls.</param>
        /// <returns>A list of each roll in string format.</returns>
        private static List<string> GetRollTextTable(string str)
        {
            return str.Split(new char[] { '+', '-' }).ToList();
        }

        /// <summary>
        /// Generates a table for the sign of each roll in the given string.
        /// </summary>
        /// <param name="str">The string to generate a sign table for.</param>
        /// <returns>A list of integers containing the generated sign table.</returns>
        private static List<int> GetSignTable(string str)
        {
            List<int> results = new List<int>
            {
                1
            };

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '+')
                {
                    results.Add(1);
                }
                else if (str[i] == '-')
                {
                    results.Add(-1);
                }
            }

            return results;
        } 
    }
}
