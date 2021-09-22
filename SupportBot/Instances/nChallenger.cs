using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengerDiscordBot.Instances
{
    class nChallenger
    {

        public ulong UserID { get; set; }
        public string Username { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsActive { get; set; }
        public double Points { get; set; }

        public nChallenger(ulong UserID, string Username, DateTime DateCreate, bool IsActive, double Points)
        {
            this.UserID = UserID;
            this.Username = Username;
            this.DateCreate = DateCreate;
            this.IsActive = IsActive;
            this.Points = Points;
        }

    }
}
