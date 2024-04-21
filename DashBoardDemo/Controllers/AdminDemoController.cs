using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Castle.Core.Smtp;

namespace DashBoardDemo.Controllers
{

    [SessionActionFilter]
    public class AdminDemoController : Controller
    {
        private readonly ILogger<AdminDemoController> _logger;
        private readonly AppDbContext appDbContext;
        private readonly IAdminDemoService adminDemoService;
        private readonly IStatisticsService statisticsService;
       
        private readonly IPieChartService pieChartService;
        //string conStr = @"Data Source=.;Initial Catalog=DoctorAppdb;User ID=sa;Password=Abbacus007";

        public AdminDemoController(ILogger<AdminDemoController> logger, AppDbContext appDbContext, IAdminDemoService adminDemoService, IStatisticsService statisticsService,  IPieChartService pieChartService)
        {
            _logger = logger;
            this.appDbContext = appDbContext;
            this.adminDemoService = adminDemoService;
            this.statisticsService = statisticsService;
           
            this.pieChartService = pieChartService;
        }
        public IActionResult Index()
        {
            return View();
        }

       
      
        //THIS IS FOR DASHBOARD CARDS
        public IActionResult DashBoard()
        {
            var totalDoctors = statisticsService.GetTotalDoctors();
            var totalPatients = statisticsService.GetTotalPatients();
            var todaysPatients = statisticsService.GetTodaysPatients();
            var approved = statisticsService.ApprovedApp();
            var rejected = statisticsService.RejectApp();

            ViewBag.TotalDoctors = totalDoctors;
            ViewBag.TotalPatients = totalPatients;
            ViewBag.TodaysPatients = todaysPatients;
            ViewBag.Approved = approved;
            ViewBag.Rejected = rejected;

            return View();
        }

        //For the Piechart and Graph
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSlot_Patients>>> GetTimeSlotPatientsRecords()
        {
            var timeslotRecords = await pieChartService.GetTimeSlotRecords();
            return Ok(timeslotRecords);
        }

        //PATIENTS WORKS
        //VIEW FOR PATIENTLIST
        public IActionResult PatientList()
        {
           
            return View();
        }
        //used for DataTable for PAtients List
        public IActionResult GetPatientsList()
        {
            var data = appDbContext.Patients.OrderByDescending(x=>x.Id).ToList();
            return new JsonResult(data);
        }

        //  BELOW TWO FOR ONE IS FOR VIDE FIRST ONE SECOND IS FOR UPDATE PATIENTS TABLE DATA
        public IActionResult EditViewPatients()
        {
            return View();
        }
        //USED TO SHOW PATIENTS LIST IN THE TABLE
        [HttpGet]
        public async Task<JsonResult> GetPatientList()
        {
            var entitydata = await adminDemoService.GetAllData();

            return Json(entitydata);
        }
        //USED FOR PUT PATIENTS VALUES FROM THE PATINET VIEW TO THE EDIT FORM 
        [HttpGet]
        public async Task<JsonResult> GetPutaPatientData(int Id)
        {

            var empdata = await adminDemoService.GetByIdPatient(Id);
            return Json(empdata);
        }
        //UPDATE IN THE PATIENT VIEW 
        [HttpPost]
        public async Task<IActionResult> Submit(Patient add)
        {
            try
            {
                var updatedPatient = await adminDemoService.Update(add);
                return Json(updatedPatient);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //DELETE FROM THE PATIENT VIEW, PATIENTS DATA
        [HttpPost]
        public async Task<JsonResult> DeleteData(int Id)
        {

            var empdata = await adminDemoService.Delete(Id);
            return Json(empdata);
        }
        
       //SEARCH IMPLEMENTATION IN THE PATINETS LIST
        [HttpPost]
        public async Task<IActionResult> Search( string PatientName)
        {
            var patient = await adminDemoService.FilterDataSearching( PatientName);
            return Json(patient);
        }




    
        //USED TO SHOW DOCTORS LIST VIEW IT IS

        public IActionResult DoctorMaster()
        {
            return View();
        }
       //Used for client side Doctor List
        public IActionResult GetDoctorList()
        {
            var data = appDbContext.Doctors.OrderByDescending(x=>x.Id).ToList();
            return new JsonResult(data);
           
        }

        //USED FOR TO SHOW DOCTORS DATA IN TABLE
        [HttpGet]
        public async Task<JsonResult> GetDoctorMaster()
        {
            var doctordata = await adminDemoService.GetAllDataDoctor();

            return Json(doctordata);
        }
        //For edit in DoctorMaster
        [HttpGet]
        public async Task<JsonResult> GetPutData(int Id)
        {

            var empdata = await adminDemoService.GetById(Id);
            return Json(empdata);
        }

        //For Add Doctors inside the Doctormaster table 
        public IActionResult Create()
        {
            return View();
        }
        //For Add Doctors inside the Doctormaster table 
        [HttpPost]
        public async Task<JsonResult> Register(Doctor std)
        {
            try
            {
                if (std.Id > 0)
                {


                    var entitydata = await adminDemoService.Update(std);

                    return Json(entitydata);

                }
                else
                {

                    var entitydata = await adminDemoService.Insert(std);

                    return Json(entitydata);
                }

                appDbContext.SaveChanges();
                return Json(true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }






        //Below two methods used to show patient TimeSlot table
        public IActionResult PatientTimeSlot()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetTimeSlotPatients()
        {
            try
            {
                var timeSlotPatients = await adminDemoService.GetPatientsTimeSlot();
                return Json(timeSlotPatients);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving data.");
            }
        }

        //WORK -> TO UPDATE ISAPPROVED COLUMN VALUE IN THE DATABASE TABLE TIMESLOT_PATIENTS AND SHOW IN TABLE APPROVED OR REJECTED
        [HttpPost]
        public async Task<IActionResult> UpdateApprovalStatus(int id, bool isApproved)
        {
            try
            {
                await adminDemoService.UpdateApprovalStatus(id, isApproved);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the approval status.");
            }
        }

        


        
        [HttpGet]
        public async Task<JsonResult> GetAllDoctorNames()
        {
            try
            {
                var doctors = await adminDemoService.GetAllDoctors();

                if (doctors != null && doctors.Any())
                {
                    var doctorNames = doctors.Select(d => new
                    {
                        doctorId = d.Id,
                        doctorName = d.DoctorName
                    }).ToList();

                    return Json(new { success = true, doctorNames });
                }
                else
                {
                    return Json(new { success = false, message = "No doctors found." });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //For Showing Time slot of doctors
        public IActionResult TimeSlotDoc()
        {
            return View();
        }
       


        //For Add DoctorsTimeSlot inside the Doctormaster table 
        public IActionResult AddDoc()
        {
            return View();
        }


        //For Insert data into  TimeSlotDoctor  table
        [HttpPost]
        public async Task<JsonResult> SubmitData(View_DoctorTimeSlot data)
        {
            try
            {
                // Check if the doctor with the provided ID exists
                var doctorExists = await adminDemoService.DoctorExists(data.DoctorId);
                if (!doctorExists)
                {
                    return Json(new { success = false, message = "Invalid Doctor ID. Please select a valid doctor." });
                }

                // Perform the insertion
                var result = await adminDemoService.InsertSlot(data);
                if (result)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to insert time slot." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while submitting data." });
            }
        }






        //To show TimeSlotDocor Data in Table

        [HttpGet]
        public async Task<IActionResult> GetTimeSlotDoctor()
        {
            try
            {
                var timeslotdoc = await adminDemoService.GetDoctorsTimeSlot();
                return Json(timeslotdoc);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        public IActionResult EditViewDocTimeSlots()
        {
            return View();
        }

        /*//EDit in Timeslot Doc

        [HttpGet]
        public async Task<IActionResult> EdiitDoctorTimeslot(int Id, int doctorId)
        {
            try
            {
                var attendanceData = await adminDemoService.GetPutDocTimeSlotData();
                var attendance = attendanceData.FirstOrDefault(a => a.Id == Id && a.DoctorId == doctorId);

                if (attendance != null)
                {
                    return Json(attendance);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
*/

        //Try
        [HttpGet]
        public async Task<IActionResult> EdiitDoctorTimeslot(int Id, int doctorId)
        {
            try
            {
                var attendanceData = await adminDemoService.GetPutDocTimeSlotData();
                var attendance = attendanceData.FirstOrDefault(a => a.Id == Id && a.DoctorId == doctorId);

                if (attendance != null)
                {
                    return Json(attendance);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }


        [HttpPost]
        public async Task<JsonResult> UpdateData(View_DoctorTimeSlot data)
        {
            try
            {
                // Check if the doctor with the provided ID exists
                var doctorExists = await adminDemoService.DoctorExists(data.DoctorId);
                if (!doctorExists)
                {
                    return Json(new { success = false, message = "Invalid Doctor ID. Please select a valid doctor." });
                }

                // Perform the update
                var result = await adminDemoService.UpdateSlot(data);
                if (result)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update time slot." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating data." });
            }
        }


    }
}
