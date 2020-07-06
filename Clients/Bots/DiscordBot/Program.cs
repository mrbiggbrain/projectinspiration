//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace DiscordBot
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    
    /// <summary>
    /// Main program loop.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Discord Client.
        /// </summary>
        private DiscordSocketClient client;

        /// <summary>
        /// Discord Service.
        /// </summary>
        private CommandService command;

        /// <summary>
        /// Run out async code on program launch. 
        /// </summary>
        /// <param name="args">Optional arguments.</param>
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Run discord bot forever.
        /// </summary>
        /// <returns>A Task.</returns>
        private async Task MainAsync()
        {
            // Create the SocketClient
            this.client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            // Create the command service
            this.command = new CommandService(new CommandServiceConfig
            {
                ////CaseSensitiveCommands = true,
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            });

            // Subscribe to MessageRecived events.
            this.client.MessageReceived += this.Client_MessageReceived;

            // Load every command from our module
            await this.command.AddModulesAsync(Assembly.GetEntryAssembly());

            // Subscribe to Client Ready requests
            this.client.Ready += this.Client_Ready;

            // Subscribe to Log Events. 
            this.client.Log += this.Client_Log;

            // Log into the client using bot token!
            string token = await File.ReadAllTextAsync("bot.key");
            await this.client.LoginAsync(TokenType.Bot, token);

            // Start Client
            await this.client.StartAsync();

            // Delay forever! 
            await Task.Delay(-1);
        }

        /// <summary>
        /// Log discord information.
        /// </summary>
        /// <param name="message">The message with information to log.</param>
        /// <returns>A Task.</returns>
        private Task Client_Log(LogMessage message)
        {
            Console.WriteLine($"{DateTime.Now} at {message.Source} | {message.Message}");
            return Task.FromResult(0);
        }

        /// <summary>
        /// Tasks to perform when the client is ready.
        /// </summary>
        /// <returns>A Task.</returns>
        private async Task Client_Ready()
        {
            await this.client.SetGameAsync("ProjectInspiration - DnD Bot");
            await this.client.SetStatusAsync(UserStatus.Online);
        }

        /// <summary>
        /// Tasks to perform when a message is received.
        /// </summary>
        /// <param name="messageParam">Handle incoming messages from discord.</param>
        /// <returns>A Task.</returns>
        private async Task Client_MessageReceived(SocketMessage messageParam)
        {
            SocketUserMessage message = messageParam as SocketUserMessage;
            SocketCommandContext context = new SocketCommandContext(this.client, message);

            if (context.Message == null || context.Message.Content == string.Empty)
            {
                return;
            }

            if (context.User.IsBot)
            {
                return;
            }

            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(this.client.CurrentUser, ref argPos)))
            {
                return;
            }

            var result = await this.command.ExecuteAsync(context, argPos);

            if (!result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands | Something went wrong with executing a command % Text = {context.Message.Content} # Error = {result.ErrorReason}");
            }
        }
    }
}
