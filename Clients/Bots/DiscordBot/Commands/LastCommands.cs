using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Discord;
using Discord.Commands;
using ProjectInspirationLibrary.Dice;
using System.Linq;
using ProjectInspirationLibrary.Dice.Parser;
using ProjectInspirationLibrary.Dice.Formater;

namespace DiscordBot.Commands
{
    public class LastCommands : ModuleBase<SocketCommandContext>
    {
        [Command("last"), Alias("l"), Summary("last roll")]
        public async Task Last([Remainder]String userText = null)
        {

            Console.WriteLine("LAST");

            String user = Context.User.Mention;

            String display = "";

            if(!String.IsNullOrEmpty(userText))
            {
                //user = userText.Replace("@", String.Empty);
                user = Context.User.Mention;


            }

            

            var data = RollSaver.Instance.Load(user);

            

            Console.WriteLine("PRE");
            if (data == null)
            {
                display = $"No Roll Data to load for {user}";
            }
            else
            {
                display = RollFormater.Format(data);
            }
            Console.WriteLine("POST");

            // Create a builder to construct the output. 
            EmbedBuilder builder = new EmbedBuilder();

            // Set author and image
            builder.WithAuthor("Dice Roller", "https://cdn.pixabay.com/photo/2017/08/31/04/01/d20-2699387_960_720.png");

            // Set outline to be red.
            builder.WithColor(Color.Red);

            // Set the description to show results. 
            builder.WithDescription($"{Context.User.Mention}: {display}");

            // Post the embed to the channel
            await Context.Channel.SendMessageAsync("", embed: builder.Build());

            // Delete the requesting message
            //await Context.Message.DeleteAsync();

            Console.WriteLine("END");
        }
    }
}