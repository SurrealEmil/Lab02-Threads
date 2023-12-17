using System.Diagnostics;
using System.Xml.Linq;

namespace Trådar
{
    internal class Program
    {
        static async Task Main()
        {
            RaceManager raceManager = new RaceManager();
            await raceManager.RunRaceAsync();

        }
    }
}
