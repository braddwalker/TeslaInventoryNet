using System.Collections.Generic;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System;

namespace TeslaInventoryNet
{
    /// <summary>
    /// This class is a light wrapper around the Tesla public inventory API. It can be used to craft inventory searches and list the results.
    /// </summary>
    public class TeslaInventory
    {
        private static readonly string TESLA_API = "https://www.tesla.com/inventory/api/v1/inventory-results";

        /// <summary>
        /// Performs a search based on the provided filter criteria.
        /// </summary>
        /// <returns>A list of search results</returns>
        public static IList<Result> Search(Location location, SearchCriteria criteria)
        {
            var query = new {
                query = new {
                    model = criteria.Model,
                    condition = criteria.Condition,
                    country = location.Country,
                    market = location.Market,
                    language = location.Language,
                    region = location.Region
                }
            };

            var client = new RestClient(TESLA_API);
            var request = new RestRequest();
            request.AddParameter("query", JsonConvert.SerializeObject(query));
            var response = client.Execute(request);

            if (response == null)
            {
                throw new Exception("Unable to call Tesla API");
            }

            var error = JsonConvert.DeserializeAnonymousType(response.Content, new { Error = "", Code = 0});
            if (error.Code > 0)
            {
                throw new Exception($"Error calling Tesla API - {error.Code}: {error.Error} - {JsonConvert.SerializeObject(query)}");
            }

            var results = JsonConvert.DeserializeAnonymousType(response.Content, new { results = new Result[0], total_matches_found = 0});

            Console.WriteLine(response.Content);

            return results.results;
        }
    }
}