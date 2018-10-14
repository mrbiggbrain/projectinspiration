using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Discord;
using Discord.Commands;
using DiscordBot.Formatters;
using ProjectInspiration.SDK.Shared.TransactionService;
using ProjectInspiration.Library.Dice.Models.Request;
using ProjectInspiration.Library.Dice.Models.Responce;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    public class RollCommands : ModuleBase<SocketCommandContext>
    {
        [Command("roll"), Alias("r"), Summary("Roll Command")]
        public async Task Roll([Remainder]String rollText = null)
        {
            // Create a new roll request
            RollRequest request = new RollRequest(rollText);

            // Create a new service to use for our web request.
            ITransactionService service = new ProjectInspirationWebService("https://localhost:44319/api", "");

            // Create variable to hold results of query. 
            IRollResult result;

            try
            {
                // Query the Transaction Service to get the result. 
                result = service.Post<RollResult, IRollRequest>(request, "dice");     
            }
            catch (JsonSerializationException e)
            {
                // Output errors with JSON serialization/Deserialization
                Console.WriteLine($"Issue: {e.Message}");

                // Need to create a new exception. 
                throw;
            }

            // Create a builder to construct the output. 
            EmbedBuilder builder = new EmbedBuilder();

            // Set author and image
            builder.WithAuthor("Dice Roller", "https://cdn.pixabay.com/photo/2017/08/31/04/01/d20-2699387_960_720.png");

            // Set outline to be red.
            builder.WithColor(Color.Red);

            // Set the description to show results. 
            builder.WithDescription($"{Context.User.Mention} Rolled a {result.Value}\n{DiscordFormatter.Format(request.RollText, bold: true, underline: true, italics: true)} :: {RollResponceFormatter.Format(result)}");

            // Post the embed to the channel
            await Context.Channel.SendMessageAsync("", embed: builder.Build());

            // Delete the requesting message
            await Context.Message.DeleteAsync();
        }
    }
}
