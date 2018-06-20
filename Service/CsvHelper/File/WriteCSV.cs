using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

public sealed class WriteCSV
{
    public void Write(List<Fuel> fuels)
    {
        if(File.Exists(@"fuel.csv"))
        {
            File.Delete(@"fuel.csv");
        }
        
        using (StreamWriter sw = new StreamWriter(@"fuel.csv"))
        {
            CsvWriter writer = new CsvWriter(sw);

            // writer.Configuration.RegisterClassMap<FuelMapping>();

            // writer.Configuration.RegisterClassMap<FuelTypePriceMapping>();
            // writer.WriteRecords(fuel);

            // foreach (Fuel record in fuels)
            // {
            //     // writer.WriteRecord(record);
            //     writer.WriteField(record.Company);
            //     writer.WriteField(record.City);
            //     writer.WriteField(record.EffectiveDate);

            //     foreach (FuelType type in record.FuelType)
            //     {
            //         writer.WriteField(type.Type);
            //         writer.WriteField(type.Price);
            //     }
            //     writer.NextRecord();
            // }
            writer.WriteHeader<Fuel>();
            Enumerable.Range(1, 5).ToList().ForEach(x => {
                writer.WriteField("Type" + x);
                writer.WriteField("Price" + x);
            });
            writer.NextRecord();
            foreach (var record in fuels)
            {
                writer.WriteRecord(record);
                foreach(FuelType ft in record.FuelType)
                {
                    writer.WriteRecord(ft);
                }
                writer.NextRecord();
            }
            writer.Flush();
        }
    }
}