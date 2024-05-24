using CarInfoBFF.Identity;
using CarInfoManagement.Models.AuthendicationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarInfoManagement.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IIdentityService authService;
        public UserAuthenticationController(IIdentityService authService)
        {
            this.authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            List<SelectListItem> option = new List<SelectListItem>();
            option = new List<SelectListItem>
            {
                new SelectListItem{Value="Admin",Text = "Admin"},
                new SelectListItem {Value = "User", Text = "User"}
            };
            ViewBag.role = option;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await authService.RegisterAsync(model);
            if (result.statusCode == 1)
                return RedirectToAction(nameof(Login));
            else
            {
                TempData["msg"] = result.message;
                return RedirectToAction(nameof(Login));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            
           
            if (result !=null && result.statusCode == 1)
            {
                HttpContext.Session.SetString("Role", result.role);
                HttpContext.Session.SetString("AccessToken", result.token);
                TempData["Role"] = HttpContext.Session.GetString("Role");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Could not logged in. Please check User Name or Password";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("AccessToken");
            TempData["Role"] = null;
            //await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
