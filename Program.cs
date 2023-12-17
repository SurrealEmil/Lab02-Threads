using System.Diagnostics;
using System.Xml.Linq;

namespace Trådar
{
    internal class Program
    {
        static async Task Main()
        {
            // Create an instance of RaceManager to manage and control the race simulation
            RaceManager raceManager = new RaceManager();

            // Asynchronously execute the race simulation using the RunRaceAsync method
            await raceManager.RunRaceAsync();

        }
    }
}
