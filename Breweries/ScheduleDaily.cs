using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace Breweries
{
    public class ScheduleDaily
    {
        public static Timer timer;
        public static void schedule_timer()
        {
            DateTime nowTime = DateTime.Now;
            DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 12, 00, 0, 0); //scheduled time as HH,MM,SS [12pm]
            if (nowTime > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            timer = new Timer(tickTime);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }
        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            
            schedule_timer();
        }



    }
}