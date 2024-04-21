using DashBoardDemo.Interface;
using DashBoardDemo.Models;
using DashBoardDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashBoardDemo.Controllers
{

    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly AppDbContext appDbContext;
        private readonly IAdminService adminService;
        private readonly IpatientLoginService ipatientLoginService;



        public AdminController(ILogger<AdminController> logger, AppDbContext appDbContext, IAdminService adminService, IpatientLoginService ipatientLoginService)
        {
            _logger = logger;
            this.appDbContext = appDbContext;
            this.adminService = adminService;
            this.ipatientLoginService = ipatientLoginService;


        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminPage()
        {
            return View();
        }


        /* [HttpPost]
         public async Task<IActionResult> Login(string UserName, string Passcode)
         {
             try
             {
                 var isSuccess = await adminService.AuthenticateUser(UserName, Passcode);
                 if (isSuccess != null)
                 {
                     HttpContext.Session.SetString("UserId", isSuccess.id.ToString());
                     return Json(true);
                 }
                 else
                 {
                     return Json(false);
                 }
             }
             catch (Exception ex)
             {

                 return StatusCode(500, "An error occurred while authenticating the user.");
             }
         }*/
        [HttpPost]
       /* public async Task<IActionResult> Login(string UserName, string Passcode)
        {
            try
            {
                var isAdmin = await adminService.AuthenticateUser(UserName, Passcode);
                if (isAdmin != null)
                {
                    HttpContext.Session.SetString("UserId", isAdmin.id.ToString());
                    return Json(true);
                }

              

                return Json(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while authenticating the user.");
            }
        }*/


        [HttpGet]
        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();

            
            return RedirectToAction("LoginPage", "Doctor"); 
        }
    }
}
