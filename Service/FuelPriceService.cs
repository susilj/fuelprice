using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using CsvHelper;
using System.IO;

internal class FuelPriceService
{
    internal async Task<IEnumerable<Fuel>> GetFuelPrice()
    {
        return await this.GetCompanyCityFuelPrice(null, null);
    }

    internal async Task<IEnumerable<Fuel>> GetCompanyCityFuelPrice(string company, string city)
    {
        ReadCSV cachedCSV = new ReadCSV();

        var cachedFuel = cachedCSV.Read();

        // cachedFuel.ForEach(x => System.Console.WriteLine(x.City));

        if (cachedFuel.Count > 0 && cachedFuel.First().EffectiveDate.AddDays(1) > DateTime.Today)
        {
            if (city != null)
            {
                return cachedFuel.Where(f => f.City == city).ToList().OrderBy(o => o.City);
            }

            return cachedFuel.OrderBy(o => o.City);
        }
        else
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.PostAsync("https://www.bharatpetroleum.com/AjaxSmartFleetMap.aspx/SetSmartFleetMap", new StringContent("", Encoding.UTF8, "application/json"));

                var responseString = await result.Content.ReadAsStringAsync();

                JObject rss = JObject.Parse(responseString);

                JArray jArray = (JArray)rss.SelectToken("d");

                List<Fuel> fuel = jArray.Select(a => new Fuel
                {
                    Company = "Bharat Petroleum",
                    City = a.SelectToken("city").Value<string>(),
                    EffectiveDate = DateTime.ParseExact(a.SelectToken("effDate").Value<string>().Replace(" Hrs", ""), "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    FuelType = ((JArray)a.SelectToken("productList")).Select(f => new FuelType
                    {
                        Type = f.SelectToken("name").Value<string>(),
                        Price = f.SelectToken("rsp").Value<decimal?>()
                    }).ToList()
                })
                .ToList();

                if (city != null)
                {
                    fuel = fuel.Where(f => f.City == city).ToList();
                }

                WriteCSV csv = new WriteCSV();

                csv.Write(fuel);

                return fuel.OrderBy(o => o.City);
            }
        }
    }
}