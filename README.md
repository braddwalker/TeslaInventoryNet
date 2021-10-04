> Retrieves real-time inventory from Tesla.

## Usage

```c#
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
```

## License

**TeslaInventoryNet** © Brad Walker, released under the [MIT](https://github.com/braddwalker/TeslaInventoryNet/blob/master/LICENSE.md) License.<br>
Authored and maintained by Brad Walker with help from [contributors](https://github.com/braddwalker/TeslaInventoryNet/contributors).

> GitHub [Brad Walker](https://github.com/braddwalker) · Twitter [@braddwalker](https://twitter.com/braddwalker)