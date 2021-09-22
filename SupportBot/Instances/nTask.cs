using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengerDiscordBot.Instances
{
    class nTask
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public int MaxPoints { get; set; }
        public string Creator { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public ulong TextMessageID { get; set; }
        public ulong TextChannelID { get; set; }

        public nTask(string Name, int MaxPoints, string Creator, DateTime DateStart, DateTime DateEnd, ulong TextMessageID, ulong TextChannelID)
        {
            this.Name = Name;
            this.MaxPoints = MaxPoints;
            this.Creator = Creator;
            this.DateStart = DateStart;
            this.DateEnd = DateEnd;
            this.TextMessageID = TextMessageID;
            this.TextChannelID = TextChannelID;
        }

    }
}
