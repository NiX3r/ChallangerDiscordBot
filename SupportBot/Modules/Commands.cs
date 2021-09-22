using ChallengerDiscordBot.Instances;
using ChallengerDiscordBot.Utils;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using SupportBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupportBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("ping"), RequireUserPermission(GuildPermission.Administrator)]
        public async Task Ping()
        {

            EmbedFieldBuilder close = new EmbedFieldBuilder();
            close.Name = "Processor";
            close.Value = HardwareUsage.getCurrentCpuUsage();
            close.IsInline = true;

            EmbedFieldBuilder log = new EmbedFieldBuilder();
            log.Name = "RAM";
            log.Value = HardwareUsage.getAvailableRAM();
            log.IsInline = true;

            EmbedFieldBuilder guilds = new EmbedFieldBuilder();
            guilds.Name = "Guilds";
            guilds.Value = ProgramVariables._client.Guilds.Count;
            guilds.IsInline = false;

            EmbedFieldBuilder latency = new EmbedFieldBuilder();
            latency.Name = "Latency";
            latency.Value = ProgramVariables._client.Latency;
            latency.IsInline = true;
            
            float cpu = (float)Convert.ToDecimal(close.Value.ToString().Replace("%", ""));

            var EmbedBuilder = new EmbedBuilder()
                .WithTitle($"Server response")
                .WithColor(cpu < 50.0 ? Color.Green : Color.Red)
                .WithThumbnailUrl(cpu < 50.0 ? "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.ytimg.com%2Fvi%2FgTrvi5MI9NY%2Fmaxresdefault.jpg&f=1&nofb=1" : "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.imgflip.com%2F4qbswj.jpg&f=1&nofb=1")
                .WithTimestamp(DateTime.Now)
                .WithFields(close, log, guilds, latency);
            Embed embed = EmbedBuilder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
            return;
        }

        [Command("set_information"), RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetInformation(IChannel channelId)
        {
            ProgramVariables.Guild.INFO_CHANNEL = channelId.Id;
            DatabaseMethods.UpdateInfoChannel(channelId.Id.ToString());
            await ReplyAsync("Information channel successfully set");
        }

        [Command("set_halloffame"), RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetHallOfFame(IChannel channelId)
        {
            ProgramVariables.Guild.FAME_CHANNEL = channelId.Id;
            DatabaseMethods.UpdateFameChannel(channelId.Id.ToString());
            await ReplyAsync("Hall of fame channel successfully set");
        }

        [Command("add_task"), RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddTask(params string[] args)
        {
            if (args.Length == 6)
            {
                string name = args[0];
                int maxPoints = Convert.ToInt32(args[1]);
                DateTime start = Convert.ToDateTime(args[2]);
                DateTime end = Convert.ToDateTime(args[3]);
                ulong channel = Convert.ToUInt64(args[4]);
                ulong message = Convert.ToUInt64(args[5]);

                nTask task = new nTask(name, maxPoints, this.Context.User.Username, start, end, message, channel);
                task.ID = DatabaseMethods.AddTask(task);

            }
            else
                await ReplyAsync("Bad input!");
        }

    }
}
