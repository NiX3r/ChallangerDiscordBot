using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBot.Events
{
    class HandleCommandAsyncEvent
    {
        public static async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(ProgramVariables._client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("chal?", ref argPos))
            {
                var result = await ProgramVariables._commands.ExecuteAsync(context, argPos, ProgramVariables._services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
