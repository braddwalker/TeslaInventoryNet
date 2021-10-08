using System;
using System.Linq;
using TeslaInventoryNet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TeslaInventoryNet.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder => {
                builder.AddConsole();
                builder.AddFilter(level => level >= LogLevel.Debug);
            });

            var logger = loggerFactory.CreateLogger<Program>();
            var tesla = new TeslaInventory(loggerFactory.CreateLogger<TeslaInventory>());

            tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "used", Count = 100},
                (results) => {
                    logger.LogInformation($"Found {results.TotalMatchesFound} vehicles total, {results.Vehicles.Length} vehicles returned");
                    
                    foreach (var result in results.Vehicles)
                    {
                        logger.LogInformation($"https://www.tesla.com/{result.Model}/order/{result.Vin}"
                                    + $"\n{result.Year} {result.Model}"
                                    + $"\n{result.TrimName}" + (result.IsDemo ? " Demo" : "")
                                    + $"\n{result.OptionCodeData.Where(x => x.Group == "PAINT").Select(x => x.Name).FirstOrDefault()}"
                                    + (result.Autopilot.Contains("AUTOPILOT_FULL_SELF_DRIVING") ? "\nFull Self-Driving Capability" : "")
                                    + $"\nFactory: {result.FactoryCode}"
                                    + $"\n{result.City}, {result.StateProvince}"
                                    + $"\n{result.CompositorUrls.FrontView}");
                    }
                });
        }
    }
}