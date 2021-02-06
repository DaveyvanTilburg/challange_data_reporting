using System;
using System.Diagnostics;
using Domain.LineGrouping;
using Domain.PointGrouping;
using Domain.ValueGrouping;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Models;

namespace Website.Controllers
{
    //Todo
    //Add support for average value per type per point
    
    //Then when that all is done, the benchmark for the design will be: Add support for point grouping by day of week
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
            => Chart("totalValue", "byMonth");

        [HttpPost]
        public IActionResult Index(string valueGrouping, string pointGrouping)
            => Chart(valueGrouping, pointGrouping);
        
        private IActionResult Chart(string valueGrouping, string pointGrouping)
        {
            ILineGrouping<Sale> lineGrouping =
                new GroupByType<Sale>(
                    new SaleDataSource()
                );

            IPointGrouping<Sale> salePointGrouping =
                pointGrouping == "byMonth" ? (IPointGrouping<Sale>)new GroupByMonth<Sale>(lineGrouping) :
                pointGrouping == "byWeek" ? new GroupByWeek<Sale>(lineGrouping) :
                throw new Exception($"Unsupported value {pointGrouping}");

            IValueGrouping saleValueGrouping =
                valueGrouping == "totalValue" ? (IValueGrouping)new SaleTotalValueGrouping(salePointGrouping) :
                valueGrouping == "averageValue" ? new SaleAverageValueGrouping(salePointGrouping) :
                    throw new Exception($"Unsupported value {pointGrouping}");

            return View(saleValueGrouping);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}