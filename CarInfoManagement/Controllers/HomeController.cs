using CarInfoManagement.Models;
using CarInfoManagement.Services.CarInfoDetails;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarInfoManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarInfoDetailsServices _carService;
        public HomeController(ICarInfoDetailsServices carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index(string term = "")
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            string accessToken = HttpContext.Session.GetString("AccessToken");
            var CarInfoDetails = await _carService.GetCarInfoDetails(accessToken,term,Convert.ToString(TempData["Role"]));
            return View(CarInfoDetails);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> CarDetail(int carId)
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            string accessToken = HttpContext.Session.GetString("AccessToken");
            var car = await _carService.GetById(carId, accessToken, Convert.ToString(TempData["Role"]));
            return View(car);
        }
        public IActionResult Error(object result)
        {
            return View();
        }

    }
}
