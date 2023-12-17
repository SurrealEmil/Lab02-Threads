using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trådar
{
    internal static class ConsoleManager
    {
        public static void DisplayCarStatus(List<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.Write($"{car.Name}: {car.Distance:F2} km, {car.Speed} km/h | ");
            }
        }
    }
}
