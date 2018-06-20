using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FuelPrice.Controllers
{
    [Route("api/[controller]")]
    public class FuelController : Controller
    {
        [HttpGet("FuelPrice")]
        public async Task<IEnumerable<Fuel>> FetchFuelPrice()
        // public IEnumerable<Fuel> FetchFuelPrice()
        {
            FuelPriceService service = new FuelPriceService();

            IEnumerable<Fuel> response = await service.GetFuelPrice();
            // IEnumerable<Fuel> response = service.GetFuelPrice();

            return response;
        }

        [HttpGet("FuelPrice/{Company}/{City}")]
        public async Task<IEnumerable<Fuel>> FetchCompanyCityFuelPrice()
        {
            FuelPriceService service = new FuelPriceService();

            IEnumerable<Fuel> response = await service.GetCompanyCityFuelPrice("BP", "Chennai");

            return response;
        }
    }
}
