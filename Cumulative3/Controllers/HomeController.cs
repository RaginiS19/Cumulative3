using System.Diagnostics;
using Cumulative3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative3.Controllers
{
    /// <summary>
    /// Controller responsible for handling requests to the Home and Privacy pages.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor that initializes the HomeController with a logger instance.
        /// </summary>
        /// <param name="logger">Injected logger for logging information and errors.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the root URL request and returns the Index view.
        /// </summary>
        /// <returns>Index view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles requests to the /Privacy URL and returns the Privacy view.
        /// </summary>
        /// <returns>Privacy view</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays an error page with diagnostic information.
        /// This method disables response caching to ensure fresh data.
        /// </summary>
        /// <returns>Error view with RequestId</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
