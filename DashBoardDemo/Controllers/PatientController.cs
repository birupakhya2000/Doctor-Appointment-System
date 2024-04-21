using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace DashBoardDemo.Controllers
{
    [SessionActionFilter]
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly AppDbContext appDbContext;
        private readonly IPatientsService patientsService;


        public PatientController(ILogger<PatientController> logger, AppDbContext appDbContext, IPatientsService patientsService)
        {
            _logger = logger;
            this.appDbContext = appDbContext;
            this.patientsService = patientsService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PatientView()
        {
            return View();
        }





        //INSERT DATA FROM THE FROM INTO PATIENTS TABLE AND VIEW_PATIENTSTIMESLOT TABLE
        /*[HttpPost]
        public async Task<JsonResult> SubmitForm(View_PatientTimeSlot data)
        {
            try
            {
                var userSessionJson = HttpContext.Session.GetString("UserSession");
                var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);

                await patientsService.InsertPatientAndTimeSlot(userSession.UserId);
                return Json(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }*/


        //Try
        [HttpPost]
        public async Task<JsonResult> SubmitForm(TimeSlot_Patients timeSlot_Patients)
        {
            try
            {
                var userSessionJson = HttpContext.Session.GetString("UserSession");
                var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);

                // Use the doctorId parameter instead of retrieving it from the URL
                

                // Add the TimeSlot record
                await patientsService.AddTimeSlot(userSession.UserId, timeSlot_Patients);

                return Json(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        [HttpGet]
        public async Task<JsonResult> GetPutData(int Id)
        {

            var empdata = await patientsService.GetByIdFor(Id);
            return Json(empdata);
        }


        [HttpGet]
        public async Task<IActionResult> GetPutPatientData()
        {
            var userSessionJson = HttpContext.Session.GetString("UserSession");
            var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);
            var patient = await patientsService.GetPatientById(userSession.UserId);
            if (patient != null)
            {
                return Json(patient);
            }
            else
            {
                return NotFound();
            }
        }



        /* [HttpPost]
         public async Task<JsonResult> InsertSlotTimePatient(View_PatientTimeSlot data)
         {
             try
             {


                  await patientsService.InsertSlot(data);
                 appDbContext.SaveChanges();



                 return Json(new { success = true });
             }
             catch (Exception ex)
             {
                 return Json(new { success = false, message = "An error occurred while submitting data." });
             }
         }*/





    }
}
    

