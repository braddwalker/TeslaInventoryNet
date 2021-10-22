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

            var culture = new CultureInfo($"{location.Language}-{location.Market}");

            tesla.Search(location, new SearchCriteria() { Model = "m3", Condition = "new", Count = 100},
                (results) => {
                    logger.LogInformation($"Found {results.TotalMatchesFound} vehicles total, {results.Vehicles.Length} vehicles returned");
                    
                    foreach (var result in results.Vehicles)
                    {
                        logger.LogInformation(result.DetailsUrl
                                    + $"\n{result.Year} {result.Model} - {result.InventoryPrice.ToString("C0", culture)}"
                                    + $"\n{result.TrimName}" + (result.IsDemo ? " Demo" : "")
                                    + $"\n{result.OptionCodeData.Where(x => x.Group == "PAINT").Select(x => x.Name).FirstOrDefault()}"
                                    + (result.OptionCodeData.Any(x => x.Group == "REAR_SEATS") ? $"\n{result.OptionCodeData.FirstOrDefault(x => x.Group == "REAR_SEATS").Name}" : "")
                                    + (result.OptionCodeData.Any(x => x.Code == "$APPB") ? $"\n{result.OptionCodeData.FirstOrDefault(x => x.Code == "$APPB").Name}" : "")
                                    + (result.OptionCodeData.Any(x => x.Code == "$APF2") ? $"\n{result.OptionCodeData.FirstOrDefault(x => x.Code == "$APF2").Name}" : "")
                                    + $"\n{result.Odometer.ToString("N0", culture)} {result.OdometerType}"
                                    + $"\n{result.SalesMetro ?? result.City}, {((location == Location.US) ? result.StateProvince : result.CountryCode)}"
                                    
                                    + $"\nFactory: {result.FactoryCode}"
                                    + $"\n{result.CompositorUrls.FrontView}\n");
                    }
                }
            );
        }
    }
}