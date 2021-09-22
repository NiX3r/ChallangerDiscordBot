using Newtonsoft.Json;
using SupportBot.Timers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBot.Events
{
    class HandleDisconnetConnectEvent
    {
        public static async Task HandleDisconnect(Exception ex)
        {

            SetGameTimer.Stop();


        }

        public static async Task HandleConnect()
        {
            
        }

    }
}
