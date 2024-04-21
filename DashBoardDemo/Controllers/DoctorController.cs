using DashBoardDemo.Interface;
using DashBoardDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DashBoardDemo.Controllers
{
    [SessionActionFilter]
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly AppDbContext appDbContext;
        private readonly IDoctorsService doctorsService;
        private readonly IPatientDataService patientDataService;
        //string conStr = @"Data Source=.;Initial Catalog=FormData;User ID=sa;Password=Abbacus007";

        public DoctorController(ILogger<DoctorController> logger, AppDbContext appDbContext, IDoctorsService doctorsService, IPatientDataService patientDataService)
        {
            _logger = logger;
            this.appDbContext = appDbContext;
            this.doctorsService = doctorsService;
            this.patientDataService = patientDataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DoctorList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetDoctors()
        {
            var doctordata = await doctorsService.GetAllDataDoctor();

            return Json(doctordata);
        }

        [HttpGet]
        public async Task<JsonResult> GetPutData(int Id)
        {

            var empdata = await doctorsService.GetById(Id);
            return Json(empdata);
        }


        //To put DoctorName in PatientView Form
        [HttpGet]
        public async Task<JsonResult> GetDoctorDetails(int Id)
        {
            try
            {
                var doctor = await doctorsService.GetById(Id);

                if (doctor != null)
                {
                    var doctorDetails = new
                    {
                        success = true,
                        doctorName = doctor.DoctorName,
                        
                    };

                    return Json(doctorDetails);
                }
                else
                {
                    return Json(new { success = false, message = "Doctor not found." });
                }
            }
            catch (Exception ex)
            {
                throw;
                
            }
        }

        

        //INSERT SLOT TIME OF DOCTORS ACCORDING TO THEIR ID IN THE PATIENTVIEW FORM
        [HttpGet]
        public IActionResult GetDoctorTimeSlots(int DoctorId, DateTime selectedDate)
        {
            try
            {
                // Validate if selectedDate is not a future date
                if (selectedDate.Date > DateTime.Today)
                {
                    return Json(new { success = false, message = "No slots available for the selected date." });
                }

                var timeSlots = doctorsService.GetDoctorTimeSlots(DoctorId,selectedDate);

                if (timeSlots != null && timeSlots.Any())
                {
                    return Json(new { success = true, timeSlots });
                }
                else
                {
                    return Json(new { success = false, message = "No slots available for the selected date." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public IActionResult PatientDataView()
        {
            return View();
        }

        public IActionResult PatientDetails()
        {
            return View();
        }

        public IActionResult TimeSlotViewPatient()
        {
            return View();
        }


        //Session used for Sigle Patient Get into the Doctor Controller
        [HttpGet]
        public async Task<IActionResult> GetPatientData()
        {
            var userSessionJson = HttpContext.Session.GetString("UserSession");
            var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);
           


            var patientData = await patientDataService.GetAttendanceWithPatientDataAsync(userSession.UserId);
            return Json(patientData);
        }

        /*[HttpGet]
        public async Task<IActionResult> GetPatientData()
        {
            var userSessionJson = HttpContext.Session.GetString("UserSession");
            var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);

            var patientData = await patientDataService.GetAttendanceWithPatientDataAsync(userSession.UserId);

            // Create a list to store patient names
            var patientNames = new List<string>();

            // Retrieve the patient names from the patientData object and add them to the list
            foreach (var patient in patientData)
            {
                var patientName = patient.Name;
                patientNames.Add(patientName);
            }

            // Pass the patient names to the front-end using ViewData
            ViewData["PatientNames"] = patientNames;

            return Json(patientData);
        }*/



        [HttpGet]
        public async Task<IActionResult> GetPatientTimeSlotData()
        {
            var userSessionJson = HttpContext.Session.GetString("UserSession");
            var userSession = JsonConvert.DeserializeObject<LoginSessionClass>(userSessionJson);
            //var employeeData = JsonConvert.DeserializeObject<EmployeMasterForm>(userId);


            var patientData = await patientDataService.GetPatientTimeslotDataAsync(userSession.UserId, userSession.UserRole);
            return Json(patientData);
        }






    }
}
