using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengerDiscordBot.Instances
{
    class nGuild
    {

        public ulong GuildID { get; set; }
        public ulong INFO_CHANNEL { get; set; }
        public ulong FAME_CHANNEL { get; set; }
        public List<nChallenger> CHALLANGERS { get; set; }
        public List<nTask> TASKS { get; set; }

        public nGuild(ulong id)
        {
            this.GuildID = id;
            CHALLANGERS = new List<nChallenger>();
            TASKS = new List<nTask>();
        }

        // Getters
        public nChallenger GetChallangerByID(ulong ID)
        {
            foreach(nChallenger ch in CHALLANGERS)
            {
                if (ch.UserID.Equals(ID))
                    return ch;
            }
            return null;
        }

        public nTask GetTaskByID(int ID)
        {
            foreach(nTask ta in TASKS)
            {
                if (ta.ID.Equals(ID))
                    return ta;
            }
            return null;
        }

    }
}
