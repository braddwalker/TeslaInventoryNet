using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Globalization;

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
            var location = Location.US;

            tesla.Search(location, new SearchCriteria() { Model = "m3", Condition = "used", Count = 100},
                (results) => {
                    logger.LogInformation($"Found {results.TotalMatchesFound} vehicles total, {results.Vehicles.Length} vehicles returned");
                    
                    foreach (var result in results.Vehicles)
                    {
                        logger.LogInformation(result.DetailsUrl
                                    + $"\n{result.Year} {result.Model} - {result.InventoryPrice.ToString("C0", new CultureInfo($"{location.Language}-{location.Market}"))}"
                                    + $"\n{result.TrimName}" + (result.IsDemo ? " Demo" : "")
                                    + $"\n{result.OptionCodeData.Where(x => x.Group == "PAINT").Select(x => x.Name).FirstOrDefault()}"
                                    + (result.Autopilot.Contains("AUTOPILOT_FULL_SELF_DRIVING") ? "\nFull Self-Driving Capability" : "")
                                    + $"\nFactory: {result.FactoryCode}"
                                    + $"\n{result.SalesMetro ?? result.City}, {((location == Location.US) ? result.StateProvince : result.CountryCode)}"
                                    + $"\n{result.CompositorUrls.FrontView}");
                    }
                }
            );
        }
    }
}