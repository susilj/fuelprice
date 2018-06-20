using System;
using System.Collections.Generic;

public class Fuel
{
    public string Company { get; set; }

    public DateTime EffectiveDate { get; set; }

    public string City { get; set; }

    public List<FuelType> FuelType { get; set; }
}