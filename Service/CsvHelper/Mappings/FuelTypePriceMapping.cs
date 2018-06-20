using CsvHelper.Configuration;

public sealed class FuelTypePriceMapping : ClassMap<FuelType>
{
    public FuelTypePriceMapping()
    {
        Map(m => m.Type);

        Map(m => m.Price);
    }
}