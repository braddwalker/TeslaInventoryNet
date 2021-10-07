> Retrieves real-time inventory from Tesla.

![Last version](https://img.shields.io/github/tag/braddwalker/TeslaInventoryNet.svg?style=flat-square)

## Usage

```c#
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
                                    + $"\nhttps://static-assets.tesla.com/v1/compositor/?model={result.Model}&view={result.CompositorViews.FrontView}&size=1441&bkba_opt=2&options={string.Join(',', result.OptionCodeData.Select(x => x.Code))}");
                    }
                });
        }
    }
```

## License

**TeslaInventoryNet** © Brad Walker, released under the [MIT](https://github.com/braddwalker/TeslaInventoryNet/blob/master/LICENSE.md) License.<br>
Authored and maintained by Brad Walker with help from [contributors](https://github.com/braddwalker/TeslaInventoryNet/contributors).

> GitHub [Brad Walker](https://github.com/braddwalker) · Twitter [@braddwalker](https://twitter.com/braddwalker)