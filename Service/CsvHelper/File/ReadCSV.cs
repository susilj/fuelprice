using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using FuelPrice.Service.CsvHelper.Mappings;

public sealed class ReadCSV
{
    public List<Fuel> Read()
    {
        if(!File.Exists(@"fuel.csv"))
            return Enumerable.Empty<Fuel>().ToList();

        using (StreamReader sr = new StreamReader(@"fuel.csv"))
        {
            using (CsvReader reader = new CsvReader(sr))
            {
                reader.Configuration.RegisterClassMap<FuelMapping>();

                return reader.GetRecords<Fuel>().ToList();
            }
        }
    }
}