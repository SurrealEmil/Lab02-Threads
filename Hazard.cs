using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Trådar
{
    internal class Hazard
    {
        public void CheckForEvent(Car car, Random random)
        {
            int chance = random.Next(1, 51); // Generate a random number between 1 and 50

            switch (chance)
            {
                case int n when n <= 1: // 1/50 chance
                    Console.WriteLine($"{car.Name} ran out of fuel! Stopping to refuel for 30 seconds.");
                    Thread.Sleep(30000); // 30 seconds
                    break;

                case int n when n <= 3: // 2/50 chance
                    Console.WriteLine($"{car.Name} got a flat tire! Stopping to change the tire for 20 seconds.");
                    Thread.Sleep(20000); // 20 seconds
                    break;

                case int n when n <= 8: // 5/50 chance
                    Console.WriteLine($"{car.Name} encountered a bird on the windshield! Stopping to clean for 10 seconds.");
                    Thread.Sleep(10000); // 10 seconds
                    break;

                case int n when n <= 18: // 10/50 chance
                    Console.WriteLine($"{car.Name} experienced an engine failure! Reducing speed by 1 km/h.");
                    car.Speed = Math.Max(0, car.Speed - 1); // Reduce speed by 1 km/h, but not below 0
                    break;
            }
        }
    }
}
