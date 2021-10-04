using System;
using System.Linq;
using TeslaInventoryNet;

namespace TeslaInventoryNet.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = TeslaInventory.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "used"});

            foreach (var result in results)
            {
                Console.WriteLine($"\nhttps://www.tesla.com/{result.Model}/order/{result.Vin}"
                            + $"\n{result.Year} {result.Model}"
                            + $"\n{result.TrimName}" + (result.IsDemo ? " Demo" : "")
                            + $"\n{result.Options.Where(x => x.Group == "PAINT").Select(x => x.Name).FirstOrDefault()}"
                            + (result.Autopilot.Contains("AUTOPILOT_FULL_SELF_DRIVING") ? "\nFull Self-Driving Capability" : "")
                            + $"\n{result.City}, {result.StateProvince}");
            }
        }
    }
}