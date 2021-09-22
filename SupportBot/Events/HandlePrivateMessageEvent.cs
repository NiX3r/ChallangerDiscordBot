using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengerDiscordBot.Events
{
    class HandlePrivateMessageEvent
    {

        public static async Task HandlePrivateMessage(SocketMessage message)
        {
            if(message.Channel.GetType().ToString().Equals("Discord.WebSocket.SocketDMChannel"))
                await message.Author.SendMessageAsync("test");
        }

    }
}
