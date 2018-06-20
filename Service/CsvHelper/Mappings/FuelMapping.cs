using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace FuelPrice.Service.CsvHelper.Mappings
{
    public sealed class FuelMapping : ClassMap<Fuel>
    {
        public FuelMapping()
        {
            // AutoMap();
            Map(m => m.Company);

            Map(m => m.City);

            Map(m => m.EffectiveDate);

            Map(m => m.FuelType).ConvertUsing(row =>
            {
                string t = "";
                decimal? p = 0;

                var list = new List<FuelType>
                {
                new FuelType { Type = row.TryGetField<string>("Type1", out t) == false ? "" : row.GetField<string>("Type1"), Price = row.TryGetField<decimal?>("Price1", out p) == false ? 0 : row.GetField<decimal?>("Price1") },
                new FuelType { Type = row.TryGetField<string>("Type2", out t) == false ? "" : row.GetField<string>("Type2"), Price = row.TryGetField<decimal?>("Price2", out p) == false ? 0 : row.GetField<decimal?>("Price2") },
                new FuelType { Type = row.TryGetField<string>("Type3", out t) == false ? "" : row.GetField<string>("Type3"), Price = row.TryGetField<decimal?>("Price3", out p) == false ? 0 : row.GetField<decimal?>("Price3") },
                new FuelType { Type = row.TryGetField<string>("Type4", out t) == false ? "" : row.GetField<string>("Type4"), Price = row.TryGetField<decimal?>("Price4", out p) == false ? 0 : row.GetField<decimal?>("Price4") }
                };

                return list.Where(f => f.Type != "").ToList();
            });
            // References<FuelTypePriceMapping>(m => m.FuelType);
        }
    }
}