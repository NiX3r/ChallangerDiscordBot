using ChallengerDiscordBot.Utils;
using Discord;
using Discord.WebSocket;
using SupportBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBot.Events
{
    class HandleReactionAddedRemovedEvent
    {
        
        public static async Task HandleReactionAsync(Cacheable<IUserMessage, ulong> cachedMessage, ISocketMessageChannel originChannel, SocketReaction reaction)
        {
            if(reaction.Emote.Name.Equals("✅") && reaction.Channel.Id.Equals(ProgramVariables.Guild.INFO_CHANNEL))
            {
                if (!DatabaseMethods.AddChallanger(reaction.UserId, reaction.User.Value.Username))
                {
                    DatabaseMethods.UpdateChallangerActivity(reaction.UserId, true);
                }
            }
        }

        public static async Task HandleReactionRemovedAsync(Cacheable<IUserMessage, ulong> cachedMessage, ISocketMessageChannel originChannel, SocketReaction reaction)
        {
            if (reaction.Emote.Name.Equals("✅") && reaction.Channel.Id.Equals(ProgramVariables.Guild.INFO_CHANNEL))
            {
                DatabaseMethods.UpdateChallangerActivity(reaction.UserId, false);
            }
        }
    }
}
