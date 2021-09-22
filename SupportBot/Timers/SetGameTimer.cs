using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SupportBot.Timers
{
    class SetGameTimer
    {

        private static Timer timer;

        public static void Setup()
        {
            timer = new Timer(60000);
            timer.Elapsed += OnTimedEvent;
        }

        public static void Start()
        {
            timer.Start();
        }

        public static void Stop()
        {
            timer.Stop();
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            int count = 0;
            ProgramVariables.Guild.CHALLANGERS.ForEach(x => { if (x.IsActive) count++; });
            ProgramVariables._client.SetGameAsync(count == 1 ? count + " challanger" : count + " challangers");
        }
    }
}
