using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JsonSorting.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using JsonSorting.JsonOperation;
using Microsoft.AspNetCore.Hosting;

namespace JsonSorting.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonService _jsonService;
        private readonly IHostingEnvironment _env;

        public HomeController(JsonService jsonService, IHostingEnvironment env)
        {
            _jsonService = jsonService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {           
            ViewBag.Message = await _jsonService.FindString();     
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData()
        {
            try
            {
                string text = await _jsonService.GetJson();
                List<UserData> result = JsonConvert.DeserializeObject<List<UserData>>(text);
                var tosendList = _jsonService.SortingBubble(result); 
                return Json(new { Result = "OK", Records = result });                 
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
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
