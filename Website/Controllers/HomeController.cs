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
    //Add support for switching between difference point grouping types
    
    //Then when that all is done, the benchmark for the design will be: Add support for point grouping by day of week
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SaleTotalValueGrouping(
                new GroupByMonth<Sale>(
                    new GroupByType<Sale>(
                        new SaleDataSource()
                    )
                )
            ));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}