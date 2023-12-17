using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Trådar
{
    internal class Car
    {
        public string Name { get; }
        public double Distance { get; private set; }
        public double Speed { get; set; }
        public bool HasFinished { get; private set; }

        public Car(string name, double speed)
        {
            Name = name;
            Distance = 0;
            Speed = speed;
            HasFinished = false;
        }

        public async Task RaceAsync(RandomEventManager randomEventManager)
        {
            const int updateIntervalMs = 100; // Update interval in milliseconds
            const int eventCheckIntervalMs = 30000; // Event check interval in milliseconds
            Stopwatch eventCheckTimer = new Stopwatch();

            eventCheckTimer.Start();

            while (!HasFinished)
            {
                UpdateDistance(updateIntervalMs);
                randomEventManager.CheckForRandomEvent(this, eventCheckTimer, eventCheckIntervalMs);

                await Task.Delay(updateIntervalMs);

                CheckRaceCompletion();
            }
        }

        private void UpdateDistance(int updateIntervalMs)
        {
            double distanceCoveredThisInterval = (Speed / 3600.0) * (updateIntervalMs / 1000.0);
            Distance += distanceCoveredThisInterval;
        }

        private void CheckRaceCompletion()
        {
            if (Distance >= 10)
            {
                HasFinished = true;
            }
        }
    }
}
