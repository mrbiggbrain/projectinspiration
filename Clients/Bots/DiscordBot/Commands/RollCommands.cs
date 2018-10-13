using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Discord;
using Discord.Commands;
using ProjectInspiration.SDK.Dice;
using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.Library.Dice;
using DiscordBot.Formatters;

namespace DiscordBot.Commands
{
    public class RollCommands : ModuleBase<SocketCommandContext>
    {
        [Command("roll"), Alias("r"), Summary("Roll Command")]
        public async Task Roll([Remainder]String rollText = null)
        {

            
            //Context.Channel.SendMessageAsync($"{Context.User.Mention} Test!");

            DiceService service = new DiceService("https://localhost:44319/api", "dice");

            RollRequest request = new RollRequest() { RollText = rollText };

            RollResponce result = RollProcessor.Roll(request);

            EmbedBuilder builder = new EmbedBuilder();

            builder.WithAuthor("Dice Roller", "https://cdn.pixabay.com/photo/2017/08/31/04/01/d20-2699387_960_720.png");
            builder.WithColor(Color.Red);
            builder.WithDescription($"{Context.User.Mention} Rolled a {result.Value}\n{DiscordFormatter.Format(request.RollText, bold: true, underline: true, italics: true)} :: {RollResponceFormatter.Format(result)}");

            await Context.Channel.SendMessageAsync("", embed: builder.Build());
            await Context.Message.DeleteAsync();

            Console.WriteLine(DiscordFormatter.Format(request.RollText, bold: true, underline: true, italics: true));
            Console.WriteLine($"RESULT: {result.Value}");

        }
    }
}
