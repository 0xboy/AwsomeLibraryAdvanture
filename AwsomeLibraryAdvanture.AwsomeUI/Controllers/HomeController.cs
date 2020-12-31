using AwsomeLibraryAdvanture.AwsomeUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwsomeLibraryAdvanture.AwsomeUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var str = await GetExternalResponse();
            JArray jarr = JArray.Parse(str);

            List<CountryCodes> CountryCodes = new List<CountryCodes>();
            foreach (var item in jarr[1])
            {
                CountryCodes.Add(new CountryCodes { CountryId = item["id"].ToString(), CountryName = item["name"].ToString() });
            }
            return View(CountryCodes);
        }

        static string _address = "http://api.worldbank.org/countries?format=json";

        private async Task<string> GetExternalResponse()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            
            return result;
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
