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
                // Update distance in correlation to the car speed
                double distanceCoveredThisInterval = (Speed / 3600.0) * (updateIntervalMs / 1000.0);
                Distance += distanceCoveredThisInterval;

                // Chans of adding random events onto car
                randomEventManager.CheckForRandomEvent(this, eventCheckTimer, eventCheckIntervalMs);

                await Task.Delay(updateIntervalMs);

                // If any car gets to 10km then loop will break
                if (Distance >= 10)
                {
                    HasFinished = true;
                }
            }
        }

        public static void DisplayCarStatus(List<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.Write($"{car.Name}: {car.Distance:F2} km, {car.Speed} km/h | ");
            }
        }
    }
}
