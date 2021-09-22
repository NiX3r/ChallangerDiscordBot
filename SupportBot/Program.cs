using Discord;
using Discord.Commands;
using Discord.WebSocket;
using SupportBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SupportBot.Timers;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using ChallengerDiscordBot.Instances;
using ChallengerDiscordBot.Utils;

namespace SupportBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = RunBotAsync();
            Console.ReadLine();
        }


        private static async Task RunBotAsync()
        {

            DatabaseMethods.SetupDatabase();
            SetGameTimer.Setup();
            ProgramVariables.ActiveEmoGrouperListeners = 0;
            ProgramVariables._client = new DiscordSocketClient();
            ProgramVariables._commands = new CommandService();
            ProgramVariables._services = new ServiceCollection().AddSingleton(ProgramVariables._client).AddSingleton(ProgramVariables._commands).BuildServiceProvider();
            ProgramVariables._client.Log += BotUtils._client_Log;
            BotUtils.RegisterPrivateMessageEvent();
            BotUtils.RegisterDisconnectConnect();
            BotUtils.RegisterGuildAvaible();
            BotUtils.RegisterReactionAdded();
            SetGameTimer.Start();
            await BotUtils.RegisterCommandsAsync();
            await ProgramVariables._client.LoginAsync(TokenType.Bot, SecretClass.GetToken());
            await ProgramVariables._client.StartAsync();
            await Task.Delay(-1);

        }

    }
}
