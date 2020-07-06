//-----------------------------------------------------------------------
// <copyright file="RollCommands.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace DiscordBot.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;
    using ProjectInspirationLibrary.Dice;
    using ProjectInspirationLibrary.Dice.Parser;

    /// <summary>
    /// Discord commands for rolling dice.
    /// </summary>
    public class RollCommands : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Roll a collection of dice once.
        /// </summary>
        /// <param name="rollText">The text representation of the roll.</param>
        /// <returns>A task.</returns>
        [Command("roll"), Alias("r"), Summary("Roll Command")]
        public async Task Roll([Remainder]string rollText = null)
        {
            if (rollText == null)
            {
                rollText = RollParser.Default();
            }

            if (RollParser.Check(rollText) != true)
            {
                RollFormater.BadRollRequest(rollText, Context);
            }
            else
            {
                List<List<RollResult>> result = RollCommands.BuildAndRoll(rollText);

                await RollFormater.RollMessage(result, Context);
            }
        }

        /// <summary>
        /// Rolls a single set of roll text multiple times.
        /// </summary>
        /// <param name="count">The number of times to perform the roll.</param>
        /// <param name="rollText">The text of the roll.</param>
        /// <returns>A task.</returns>
        [Command("mroll"), Alias("mr"), Summary("Roll Command")]
        public async Task MRoll(int count, [Remainder]string rollText = null)
        {
            Console.WriteLine(count);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i);
                await this.Roll(rollText);
            }
        }

        /// <summary>
        /// Death Save Command. 
        /// </summary>
        /// <returns>A Task.</returns>
        [Command("death"), Alias("dth"), Summary("Death Save")]
        public async Task Death()
        {
            List<List<RollResult>> result = RollCommands.BuildAndRoll("1d20");

            // Create a builder to construct the output. 
            EmbedBuilder builder = new EmbedBuilder();

            // Set author and image
            builder.WithAuthor("Deaths Door", "https://cdn.pixabay.com/photo/2014/03/25/15/17/cross-296395_1280.png");

            // Set outline to be red.
            builder.WithColor(Color.DarkerGrey);

            int total = result.Sum(x => x.Sum(y => y.Result));

            string status = total >= 10 ? "Success" : "Failure";

            // Set the description to show results. 
            builder.WithDescription($"{Context.User.Mention}: {total} = {status}");

            await Context.Channel.SendMessageAsync(string.Empty, embed: builder.Build());
        }

        /// <summary>
        /// Generate the RollBuilder and use it to roll.
        /// </summary>
        /// <param name="rollText">Text to use for building the roll.</param>
        /// <returns>A <see cref="List{List{RollResult}} containg the results of the roll." /></returns>
        private static List<List<RollResult>> BuildAndRoll(string rollText)
        {
            RollBuilder build = RollParser.Parse(rollText);
            return build.Roll();
        }
    }
}
