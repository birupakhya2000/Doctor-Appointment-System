using DashBoardDemo.Interface;
using DashBoardDemo.Interface.Interface_ServerSide;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using DashBoardDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DashBoardDemo.Controllers
{
    [SessionActionFilter]
    public class ServerSideController : Controller
    {
        private readonly IServerSideDoctorsListService serverSideDoctorsListService;
        private readonly IServerSidePatientsListService serverSidePatientsListService;
        private readonly IServerSide_PatientsTimeSlotService serverSide_PatientsTimeSlotService;
        private readonly IServerSide_DoctorsTimeSlotService serverSide_DoctorsTimeSlotService;


        public ServerSideController(IServerSideDoctorsListService serverSideDoctorsListService, IServerSidePatientsListService serverSidePatientsListService, IServerSide_PatientsTimeSlotService serverSide_PatientsTimeSlotService, IServerSide_DoctorsTimeSlotService serverSide_DoctorsTimeSlotService)
        {
            this.serverSideDoctorsListService = serverSideDoctorsListService;
            this.serverSidePatientsListService = serverSidePatientsListService;
            this.serverSide_PatientsTimeSlotService = serverSide_PatientsTimeSlotService;
            this.serverSide_DoctorsTimeSlotService = serverSide_DoctorsTimeSlotService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ServerSideDoctorsList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetDoctorsList()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            var data = serverSideDoctorsListService.GetDoctorList(searchValue,sortColumn,sortColumnDirection);
            var totalRecords = data.Count();
            var filteredRecords = totalRecords;
            var employeeList = data.Skip(start).Take(length).ToList();

            var response = new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = employeeList
            };

            return Json(response);
        }


        //
        //For edit in DoctorMaster
        [HttpGet]
        public async Task<JsonResult> GetPutData(int Id)
        {

            var empdata = await serverSideDoctorsListService.GetById(Id);
            return Json(empdata);
        }

        //For Add Doctors inside the Doctormaster table 
        public IActionResult DoctorRegdEdit()
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


                    var entitydata = await serverSideDoctorsListService.Update(std);

                    return Json(entitydata);

                }
                else
                {

                    var entitydata = await serverSideDoctorsListService.Insert(std);

                    return Json(entitydata);
                }

               
                return Json(true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //Searching in Doctors by their name
        [HttpPost]
        public async Task<IActionResult> Search(string doctorName, DataTableRequest request)
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var doctors = await serverSideDoctorsListService.FilterDataSearching(doctorName, request, searchValue, sortColumn, sortColumnDirection);
            return Json(doctors);
        }





        public IActionResult ServerSidePatientsList()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GetPatientsList()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var sortColumn = Request.Form[$"columns[{Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            var data = serverSidePatientsListService.GetPatientsList(searchValue, sortColumn, sortColumnDirection);
            var totalRecords = data.Count();
            var filteredRecords = totalRecords;
            var employeeList = data.Skip(start).Take(length).ToList();

            var response = new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = employeeList
            };

            return Json(response);
        }


        public IActionResult EditViewPatients()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Patient add)
        {
            try
            {
                var updatedPatient = await serverSidePatientsListService.Update(add);
                return Json(updatedPatient);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteData(int Id)
        {

            var empdata = await serverSidePatientsListService.Delete(Id);
            return Json(empdata);
        }



        //USED FOR PUT PATIENTS VALUES FROM THE PATINET VIEW TO THE EDIT FORM 
        [HttpGet]
        public async Task<JsonResult> GetPutaPatientData(int Id)
        {

            var empdata = await serverSidePatientsListService.GetByIdPatient(Id);
            return Json(empdata);
        }

        public IActionResult ServerSidePatientTimeSlot()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPatientsTimneSlot(DataTableRequest request)
        {
            var sortColumn = Request.Form[$"columns[{Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var response = await serverSide_PatientsTimeSlotService.GetPatientsSlotTime(request, sortColumn, sortColumnDirection);

            return Json(response);
        }


        public IActionResult ServerSideDoctorsTimeSlot()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> GetDoctorsTimeSlot(DataTableRequest request)
        {
            var sortColumn = Request.Form[$"columns[{Request.Form["order[0][column]"].FirstOrDefault()}][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var response = await serverSide_DoctorsTimeSlotService.GetDoctorsSlotTime(request, sortColumn, sortColumnDirection);

            return Json(response);
        }

        //Used to show all doctor names in a dropdown in AddDoc view
        [HttpGet]
        public async Task<JsonResult> GetAllDoctorNames()
        {
            try
            {
                var doctors = await serverSide_DoctorsTimeSlotService.GetAllDoctors();

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
                var doctorExists = await serverSide_DoctorsTimeSlotService.DoctorExists(data.DoctorId);
                if (!doctorExists)
                {
                    return Json(new { success = false, message = "Invalid Doctor ID. Please select a valid doctor." });
                }

                // Perform the insertion
                var result = await serverSide_DoctorsTimeSlotService.InsertSlot(data);
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

        public IActionResult EditViewDocTimeSlots()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EdiitDoctorTimeslot(int Id, int doctorId)
        {
            try
            {
                var attendanceData = await serverSide_DoctorsTimeSlotService.GetPutDocTimeSlotData();
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
                var doctorExists = await serverSide_DoctorsTimeSlotService.DoctorExists(data.DoctorId);
                if (!doctorExists)
                {
                    return Json(new { success = false, message = "Invalid Doctor ID. Please select a valid doctor." });
                }

                // Perform the update
                var result = await serverSide_DoctorsTimeSlotService.UpdateSlot(data);
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
