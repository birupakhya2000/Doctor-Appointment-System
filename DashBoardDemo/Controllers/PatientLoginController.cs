using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using DashBoardDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DashBoardDemo.Controllers
{
    public class PatientLoginController : Controller
    {
        private readonly ILogger<PatientLoginController> _logger;
        private readonly AppDbContext appDbContext;
        private readonly IpatientLoginService ipatientLoginService;
        private readonly IAdminService adminService;
        private readonly PatientsRepos patientsRepos;




        public PatientLoginController(ILogger<PatientLoginController> logger, AppDbContext appDbContext, IpatientLoginService ipatientLoginService, IAdminService adminService, PatientsRepos patientsRepos)
        {
            _logger = logger;
            this.appDbContext = appDbContext;
            this.ipatientLoginService = ipatientLoginService;
            this.adminService = adminService;
            this.patientsRepos = patientsRepos;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        public IActionResult PatientRegister()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SubmitForm(Patient patientViewModel, PatientLogin patientLoginViewModel)
        {
            try
            {
                await ipatientLoginService.InsertPatientAndTimeSlot(patientViewModel, patientLoginViewModel);
                return Json(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckDuplicacyEmailForPatients(string email)
        {
            bool UserExist = await ipatientLoginService.CheckDuplicacyForEmail(email);

            if (UserExist)
            {
                // Email is valid
                // Perform further actions or redirect to the next step
                return Ok(true);
            }
            else
            {
                // Email is not valid
                // Display an error message or redirect to an error page
                return Ok(false);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CheckDuplicacyUserNamePatients(string username)
        {
            bool UserExist = await ipatientLoginService.CheckDuplicacyForUserName(username);

            if (UserExist)
            {
                // Email is valid
                // Perform further actions or redirect to the next step
                return Ok(true);
            }
            else
            {
                // Email is not valid
                // Display an error message or redirect to an error page
                return Ok(false);
            }
        }



        [HttpPost]
        public IActionResult Login(PatientLogin model)
        {
            // Perform authentication and verify user credentials
            var isAuthenticated = ipatientLoginService.AuthenticateUser(model.UserName, model.passcode);

            if (isAuthenticated != null)
            {
                // Retrieve the user's role from the database based on their username
                string role = isAuthenticated.UserRole;

                var userSession = new { UserId = isAuthenticated.PatientId, UserRole = isAuthenticated.UserRole };
                var userSessionJson = JsonConvert.SerializeObject(userSession);
                HttpContext.Session.SetString("UserSession", userSessionJson);

                // Store the user's role in a session variable
                //HttpContext.Session.SetString("UserRole", role);

                // Redirect the user based on their role
                if (role != null)
                {
                    return Json(new { message = role });
                }
                else
                {
                    return Json(new { message = "unknown" });
                }

            }
            else
            {
                // Authentication failed, show an error message
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }
        }





        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();


            return RedirectToAction("LoginPage", "PatientLogin");
        }


        public IActionResult PatientDashBoard()
        {
            var userSessionJson = HttpContext.Session.GetString("UserSession");
            var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);
            var patientId = userSession.UserId;

            if (patientId != null)
            {
                // Retrieve the employee name from the database
                var patientName = ipatientLoginService.GetPatientName(patientId);

                // Pass the employee name to the view
                ViewBag.PatientName = patientName;
            }
            return View();
        }

    }
}

