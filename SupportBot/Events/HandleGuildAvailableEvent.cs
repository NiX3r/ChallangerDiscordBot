using ChallengerDiscordBot.Instances;
using ChallengerDiscordBot.Utils;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBot.Events
{
    class HandleGuildAvailableEvent
    {

        public static async Task HandleCommandAsync(SocketGuild arg)
        {

            if (!arg.Name.Equals("nCodes"))
            {
                await arg.LeaveAsync();
                return;
            }

            ProgramVariables.Guild = new nGuild(arg.Id);
            DatabaseMethods.LoadData();

        }

    }
}
