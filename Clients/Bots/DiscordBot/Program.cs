using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;

namespace DiscordBot
{
    class Program
    {
        // Create a discord client
        private DiscordSocketClient client;

        // Create a discord command service
        private CommandService command;

        // Run out async code on program launch. 
        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        // Async Code
        private async Task MainAsync()
        {
            // Create the SocketClient
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            // Create the command service
            command = new CommandService(new CommandServiceConfig
            {
                //CaseSensitiveCommands = true,
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Info
            });

            // Subscribe to MessageRecived events.
            client.MessageReceived += Client_MessageReceived;

            // Load every command from our module
            await command.AddModulesAsync(Assembly.GetEntryAssembly());

            // Subscribe to Client Ready requests
            client.Ready += Client_Ready;

            // Subscribe to Log Events. 
            client.Log += Client_Log;

            // Log into the client using bot token!
            String token = await File.ReadAllTextAsync("bot.key");
            await client.LoginAsync(TokenType.Bot, token);

            // Start Client
            await client.StartAsync();

            // Delay forever! 
            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source} | {Message.Message}");
        }

        private async Task Client_Ready()
        {
            await client.SetGameAsync("ProjectInspiration - DnD Bot");
            await client.SetStatusAsync(UserStatus.Online);
        }

        private async Task Client_MessageReceived(SocketMessage messageParam)
        {
            SocketUserMessage message = messageParam as SocketUserMessage;
            SocketCommandContext context = new SocketCommandContext(client, message);

            if (context.Message == null || context.Message.Content == "") return;
            if (context.User.IsBot) return;

            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var result = await command.ExecuteAsync(context, argPos);

            if(!result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands | Something went wrong with executing a command % Text = {context.Message.Content} # Error = {result.ErrorReason}");
            }
        }
    }
}
