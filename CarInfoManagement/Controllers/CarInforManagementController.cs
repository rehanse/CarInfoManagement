using CarInfoManagement.Services.CarInfoDetails;
using CarInfoManagement.Services;
using Microsoft.AspNetCore.Mvc;
using CarInfoManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace CarInfoManagement.Controllers
{
    public class CarInforManagementController : Controller
    {
        private ICarInfoDetailsServices _carInfoDetailsServices;
        private IFileServices _fileService;
        public CarInforManagementController(ICarInfoDetailsServices carInfoDetailsServices, IFileServices fileServices)
        {
            this._carInfoDetailsServices = carInfoDetailsServices;
            this._fileService = fileServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CarList()
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(Convert.ToString(TempData["Role"])))
            {
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Remove("AccessToken");
                TempData["Role"] = null;
                return RedirectToAction("Login", "UserAuthentication");
            }
            var CarInfoDetails = await _carInfoDetailsServices.GetCarInfoDetails(HttpContext.Session.GetString("AccessToken"), "", Convert.ToString(TempData["Role"]));
            return View(CarInfoDetails);
        }
        [HttpGet]
        public async Task<IActionResult> AddCar()
        {
            var model = new CarViewModel();
            TempData["Role"] = HttpContext.Session.GetString("Role");
            if (Convert.ToString(TempData["Role"]) == "Admin")
            {
                model.accessToken = HttpContext.Session.GetString("AccessToken");
                model.carReferenceViewModel = await _carInfoDetailsServices.GetListOfCarReference(model.accessToken, Convert.ToString(TempData["Role"]));
                ViewBag.Manufacturer = model.carReferenceViewModel.manufacturer
                         .Select(i => new SelectListItem
                         {
                             Value = i.id.ToString(),
                             Text = i.name
                         }).ToList();
                ViewBag.CarType = model.carReferenceViewModel.carType
                         .Select(i => new SelectListItem
                         {
                             Value = i.id.ToString(),
                             Text = i.type
                         }).ToList();
                ViewBag.CarTransmission = model.carReferenceViewModel.cartransmisionTypes
                         .Select(i => new SelectListItem
                         {
                             Value = i.id.ToString(),
                             Text = i.name
                         }).ToList();

            }
            else
            {
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Remove("AccessToken");
                TempData["Role"] = null;
                return RedirectToAction("Login", "UserAuthentication");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(CarViewModel model)
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            model.accessToken = HttpContext.Session.GetString("AccessToken");
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.carImage = imageName;
            }
            if (Convert.ToString(TempData["Role"]) != "Admin")
            {
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Remove("AccessToken");
                TempData["Role"] = null;
                return RedirectToAction("Login", "UserAuthentication");
            }
            var result = await _carInfoDetailsServices.Add(model);
            if (result.statusCode == 1)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(AddCar));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditCarDetails(int Id)
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            var model = new CarViewModel();
            if (Convert.ToString(TempData["Role"]) == "Admin")
            {
                model = await _carInfoDetailsServices.GetById(Id, HttpContext.Session.GetString("AccessToken"), Convert.ToString(TempData["Role"]));
                model.accessToken = HttpContext.Session.GetString("AccessToken");
                model.carReferenceViewModel = await _carInfoDetailsServices.GetListOfCarReference(model.accessToken, Convert.ToString(TempData["Role"]));
                ViewBag.Manufacturer = model.carReferenceViewModel.manufacturer
                         .Select(i => new SelectListItem
                         {
                             Value = i.id.ToString(),
                             Text = i.name
                         }).ToList();
                ViewBag.CarType = model.carReferenceViewModel.carType
                         .Select(i => new SelectListItem
                         {
                             Value = i.id.ToString(),
                             Text = i.type
                         }).ToList();
                ViewBag.CarTransmission = model.carReferenceViewModel.cartransmisionTypes
                         .Select(i => new SelectListItem
                         {
                             Value = i.id.ToString(),
                             Text = i.name
                         }).ToList();
            }
            else
            {
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Remove("AccessToken");
                TempData["Role"] = null;
                return RedirectToAction("Login", "UserAuthentication");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCarDetails(CarViewModel model)
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            model.accessToken = HttpContext.Session.GetString("AccessToken");
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.carImage = imageName;
            }
            var result = await _carInfoDetailsServices.Update(model.Id, model);
            if (result != null)
            {
                return RedirectToAction(nameof(CarList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["Role"] = HttpContext.Session.GetString("Role");
            if (Convert.ToString(TempData["Role"]) == "Admin")
            {

                var result = await _carInfoDetailsServices.Delete(id, HttpContext.Session.GetString("AccessToken"));
            }
            else
            {
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Remove("AccessToken");
                TempData["Role"] = null;
                return RedirectToAction("Login", "UserAuthentication");
            }
            return RedirectToAction(nameof(CarList));
        }
    }
}
