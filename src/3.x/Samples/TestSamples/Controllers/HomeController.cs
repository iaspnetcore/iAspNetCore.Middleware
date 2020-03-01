using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using TestSamples.Models;



namespace TestSamples.Controllers
{
    public class HomeController : Controller
    {

        #region Fields
      
        private readonly ILogger _logger;



        //     private readonly IFileConvertService _fileConvertService;
        //     private IdentityOptions options;
        #endregion


        #region Ctor

        public HomeController(
            ILogger<HomeController> logger
            )
        {

            
            this._logger = logger;
           


        }



        #endregion


        public IActionResult Index()
        {
            this._logger.LogInformation("ddd");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
