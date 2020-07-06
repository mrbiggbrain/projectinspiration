//-----------------------------------------------------------------------
// <copyright file="RollFormater.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace DiscordBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;
    using ProjectInspirationLibrary.Dice;

    /// <summary>
    /// Class used for formatting rolls for Discord. 
    /// </summary>
    public static class RollFormater
    {
        /// <summary>
        /// Format the results of rolls into a string.
        /// </summary>
        /// <param name="result">The results of various rolls.</param>
        /// <returns>A string representing the rolls.</returns>
        public static String Format(List<List<RollResult>> result)
        {
            var setsTextList = new List<string>();

            foreach (var set in result)
            {
                setsTextList.Add(RollFormater.FormatSet(set));
            }

            var totalSum = result.Sum(x => x.Where(y => y.Valid).Sum(z => z.Result));
            var setsText = string.Join(", ", setsTextList);

            return $"{setsText} = {totalSum}";
        }

        public static async Task BadRollRequest(string rollText, SocketCommandContext context)
        {

            // Create a builder to construct the output. 
            EmbedBuilder builder = new EmbedBuilder();

            // Set author and image
            builder.WithAuthor("Dice Roller", "https://cdn.pixabay.com/photo/2017/02/12/21/29/false-2061132_960_720.png");

            // Set outline to be red.
            builder.WithColor(Color.DarkOrange);

            // Set the description to show results. 
            builder.WithDescription($"{context.User.Mention}: **{rollText}** does not look like a valid roll.");

            await context.Channel.SendMessageAsync(string.Empty, embed: builder.Build());

        }

        public static async Task RollMessage(List<List<RollResult>> result, SocketCommandContext context)
        {
            EmbedBuilder builder = RollFormater.GenerateDiscordBuilder(result, context);

            // Post the embed to the channel
            await context.Channel.SendMessageAsync(string.Empty, embed: builder.Build());
        }

        /// <summary>
        /// Generates a discord builder that can be sent via a message.
        /// </summary>
        /// <param name="result">The results of various rolls.</param>
        /// <param name="context">The context of the original request.</param>
        /// <returns>A builder that is ready to be used.</returns>
        public static EmbedBuilder GenerateDiscordBuilder(List<List<RollResult>> result, SocketCommandContext context)
        {
            string display = RollFormater.Format(result);

            // Create a builder to construct the output. 
            EmbedBuilder builder = new EmbedBuilder();

            // Set author and image
            builder.WithAuthor("Dice Roller", "https://cdn.pixabay.com/photo/2017/08/31/04/01/d20-2699387_960_720.png");

            // Set outline to be red.
            builder.WithColor(Color.Red);

            // Set the description to show results. 
            builder.WithDescription($"{context.User.Mention}: {display}");

            return builder;    
        }

        /// <summary>
        /// Formats a single set into it's string representation.
        /// </summary>
        /// <param name="set">The set to format.</param>
        /// <returns>A string representing the set.</returns>
        private static string FormatSet(List<RollResult> set)
        {
            List<string> rollTextList = new List<string>();

            foreach (var roll in set)
            {
                rollTextList.Add(RollFormater.FormatRoll(roll));
            }

            var rollsText = string.Join(", ", rollTextList);

            return (set.Count > 1) ? $"({rollsText})" : rollsText;
        }

        /// <summary>
        /// Formats a single roll into it's string representation.
        /// </summary>
        /// <param name="roll">The roll to format.</param>
        /// <returns>A string representation of the roll.</returns>
        private static string FormatRoll(RollResult roll)
        {
            string rollText = $"{roll.Result}";
            rollText = !roll.Valid ? $"~~{rollText}~~" : rollText;
            rollText = roll.IsCrit() ? $"**{rollText}**" : rollText;

            return rollText;
        }
    }
}
