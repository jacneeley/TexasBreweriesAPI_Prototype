using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace Breweries
{
    public class ScheduleDaily
    {
        public static Timer aTimer = new System.Timers.Timer(24 * 60 * 60 * 1000);
     //   aTimer.Elapsed += new ElapsedEventHandler(ExecuteEveryDayMethod);
    //    aTimer.Enabled = true;



    }
}