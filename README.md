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
                            + $"\n{result.CompositorUrls.FrontView}");
                    }
                });
        }
    }
```

## API
### TeslaInventory(logger, [Optional]restClientImpl])
#### logger
Type: `ILogger<TeslaInventory>`
The logger to use to log any debug/info messages

#### restClientImpl
Type: `IRestClient`
An optional `IRestClient` implementation to use for invoking API calls

### Search(location, searchCriteria)
#### location
Type: `Location`
An instance of a `Location` object that represents the country/region to be searched.

#### searchCriteria
Type: `SearchCriteria`
The search criteria to perform the search against.
- `Model`: m3 | ms | my | mx
- `Condition`: new | used
- `Count`: The number of results to return. Default is 20
- `Offset`: For paging, the index into the total results to begin from

## License

**TeslaInventoryNet** © Brad Walker, released under the [MIT](https://github.com/braddwalker/TeslaInventoryNet/blob/master/LICENSE.md) License.<br>
Authored and maintained by Brad Walker with help from [contributors](https://github.com/braddwalker/TeslaInventoryNet/contributors).

> GitHub [Brad Walker](https://github.com/braddwalker) · Twitter [@braddwalker](https://twitter.com/braddwalker)