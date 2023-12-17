using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trådar
{
    internal class RandomEventManager
    {
        public void CheckForRandomEvent(Car car, Stopwatch eventCheckTimer, int eventCheckIntervalMs)
        {
            Random random = new Random();
            Hazard hazard = new Hazard(); // Create an instance of Hazard

            if (eventCheckTimer.ElapsedMilliseconds >= eventCheckIntervalMs)
            {
                hazard.CheckForEvent(car, random);
                eventCheckTimer.Restart();
            }
        }
    }
}
