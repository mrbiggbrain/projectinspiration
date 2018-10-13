using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Discord;
using Discord.Commands;
using ProjectInspiration.SDK.Dice;
using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.Library.Dice;

namespace DiscordBot.Commands
{
    public class ComingCommands : ModuleBase<SocketCommandContext>
    {
        [Command("coming"), Alias("here"), Summary("comming Command")]
        public async Task Coming([Remainder]String rollText = null)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithAuthor("Coming!", "https://cdn.pixabay.com/photo/2017/08/31/04/01/d20-2699387_960_720.png");
            builder.WithColor(Color.Green);
            builder.WithDescription($"{Context.User.Mention} Is Coming!");

            await Context.Channel.SendMessageAsync("", embed: builder.Build());
        }
    }
}
