﻿using Discord;
using Discord.Commands;
using ProjectInspirationLibrary.Dice;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class DiagnosticCommands : ModuleBase<SocketCommandContext>
    {
        [Command("diagnostic"), Alias("d"), Summary("Diagnostic Command")]
        public async Task Roll([Remainder]String rollText = null)
        {



            // Create a builder to construct the output. 
            EmbedBuilder builder = new EmbedBuilder();

            // Set author and image
            //builder.WithAuthor("Dice Roller", "https://cdn.pixabay.com/photo/2017/08/31/04/01/d20-2699387_960_720.png");

            // Set outline to be red.
            //builder.WithColor(Color.Red);

            // Set the description to show results. 
            builder.WithDescription($"@<!{Context.User.Id}>");

            // Post the embed to the channel
            await Context.Channel.SendMessageAsync("", embed: builder.Build());

            // Delete the requesting message
            //await Context.Message.DeleteAsync();
        }
    }
}