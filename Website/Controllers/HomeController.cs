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
                pointGrouping == "byMonth" ? new GroupByMonth<Sale>(lineGrouping) :
                pointGrouping == "byWeek" ? (IPointGrouping<Sale>)new GroupByWeek<Sale>(lineGrouping) :
                pointGrouping == "byDayOfWeek" ? new GroupByDayOfWeek<Sale>(lineGrouping) :
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