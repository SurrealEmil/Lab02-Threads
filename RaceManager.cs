using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trådar
{
    internal class RaceManager
    {
        private List<Car> allCars;
        private List<Task> allRaceTasks;

        public RaceManager()
        {
            allCars = CreateCars();
            allRaceTasks = StartRaceTasks(allCars);
        }

        public async Task RunRaceAsync()
        {
            DisplayRaceStartMessage(allCars);

            await MonitorRaceStatus();

            // Use Task.WhenAny to wait for any one of the races to finish
            Task firstRaceToFinish = await Task.WhenAny(allRaceTasks);

            await DisplayRaceResults(firstRaceToFinish);
        }

        private List<Car> CreateCars()
        {
            return new List<Car>
            {
                new Car("DeLorean", 120),
                new Car("Ecto-1", 120),
                new Car("Batmobile", 120),
            };
        }

        private List<Task> StartRaceTasks(List<Car> cars)
        {
            List<Task> raceTasks = new List<Task>();
            RandomEventManager randomEventManager = new RandomEventManager();

            foreach (var car in cars)
            {
                Task raceTask = Task.Run(() => car.RaceAsync(randomEventManager));
                raceTasks.Add(raceTask);
            }

            return raceTasks;
        }

        private void DisplayRaceStartMessage(List<Car> cars)
        {
            Console.WriteLine("\t\t\t  The race has started\n" +
                              "To check the race status either wait 30 sec or type 'status' and press Enter:");

            ConsoleManager.DisplayCarStatus(cars);
            Console.WriteLine();
        }

        private async Task MonitorRaceStatus()
        {
            Stopwatch statusUpdateTimer = new Stopwatch();
            statusUpdateTimer.Start();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "status")
                    {
                        ConsoleManager.DisplayCarStatus(allCars);
                        Console.WriteLine();
                    }
                }
                // Check and display race status every 30 seconds
                if (statusUpdateTimer.ElapsedMilliseconds >= 30000)
                {
                    ConsoleManager.DisplayCarStatus(allCars);
                    Console.WriteLine();
                    statusUpdateTimer.Restart();
                }

                if (allCars.Any(car => car.HasFinished))
                {
                    Console.WriteLine(); // Move to the next line
                    break;
                }

                await Task.Delay(100);
            }
        }

        private async Task DisplayRaceResults(Task firstRaceToFinish)
        {
            Console.Clear();
            ConsoleManager.DisplayCarStatus(allCars);
            await Console.Out.WriteLineAsync();

            int winningCarIndex = allRaceTasks.IndexOf(firstRaceToFinish);

            if (winningCarIndex >= 0 && winningCarIndex < allCars.Count)
            {
                var winningCar = allCars[winningCarIndex];
                await Console.Out.WriteLineAsync($"{winningCar.Name} won the race!");
            }
        }
    }
}
