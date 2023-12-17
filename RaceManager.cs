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
        private List<Car> allCars; // List to store all the cars participating in the race
        private List<Task> allRaceTasks; // List to store the asynchronous race tasks for each car

        public RaceManager()
        {
            allCars = CreateCars(); // Initialize the list of cars using the CreateCars method
            allRaceTasks = StartRaceTasks(allCars); // Initialize the list of race tasks using the StartRaceTasks method
        }

        // Asynchronous method to run the race simulation
        public async Task RunRaceAsync()
        {
            DisplayRaceStartMessage(); // Display the race start message

            await MonitorRaceStatus(); // Asynchronously monitor the race status

            // Use Task.WhenAny to wait for any one of the races to finish
            Task firstRaceToFinish = await Task.WhenAny(allRaceTasks);

            await DisplayRaceResults(firstRaceToFinish); // Display the race results
        }

        // Method to create a list of cars for the race simulation
        // You can easily add as many cars as needed without changing any of the other code, making the program modular and scalable
        // Note: The speed values represent the initial speeds of the cars.
        private List<Car> CreateCars()
        {
            return new List<Car>
            {
                new Car("DeLorean", 120),
                new Car("Ecto-1", 120),
                new Car("Batmobile", 120),
                // Add more cars here if desired
            };
        }

        private List<Task> StartRaceTasks(List<Car> cars)
        {
            List<Task> raceTasks = new List<Task>();
            RandomEventManager randomEventManager = new RandomEventManager();

            // Start a race task for each car using Task.Run
            foreach (var car in cars)
            {
                Task raceTask = Task.Run(() => car.RaceAsync(randomEventManager));
                raceTasks.Add(raceTask);
            }

            return raceTasks; // Return the list of race tasks
        }

        private void DisplayRaceStartMessage()
        {
            Console.WriteLine("\t\tThe race has started: First to 10km wins\n" +
                              "To check the race status either wait 30 sec or type 'status' and press Enter:");

            Car.DisplayCarStatus(allCars); // Display the initial status of each car
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
                        Car.DisplayCarStatus(allCars);
                        Console.WriteLine();
                    }
                }
                // Check and display race status every 30 seconds
                if (statusUpdateTimer.ElapsedMilliseconds >= 30000)
                {
                    Car.DisplayCarStatus(allCars);
                    Console.WriteLine();
                    statusUpdateTimer.Restart();
                }

                // Check if any car has finished the race
                if (allCars.Any(car => car.HasFinished))
                {
                    Console.WriteLine(); // Move to the next line
                    break;
                }

                await Task.Delay(100); // Introduce a short delay to avoid excessive CPU usage
            }
        }

        private async Task DisplayRaceResults(Task firstRaceToFinish)
        {
            Console.Clear();
            Car.DisplayCarStatus(allCars); // Display the final status of each car
            await Console.Out.WriteLineAsync();

            // Determine the index of the winning car in the list of race tasks
            int winningCarIndex = allRaceTasks.IndexOf(firstRaceToFinish);

            // Display the winning car's name if it's a valid index
            if (winningCarIndex >= 0 && winningCarIndex < allCars.Count)
            {
                var winningCar = allCars[winningCarIndex];
                await Console.Out.WriteLineAsync($"{winningCar.Name} won the race!");
            }
        }
    }
}
