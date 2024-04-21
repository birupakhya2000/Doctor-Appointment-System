using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.Interface
{
    public interface IAdminDemoService
    {
       
        Task<List<Patients>> GetAllData();
        Task<List<Doctors>> GetAllDataDoctor();
        Task<Doctors> GetById(int Id);
        Task<List<Patients>> Delete(int Id);
        Task<List<TimeSlot_Patients>> GetAllTimeSlot();

        //SEARCH FOR USING 3 THINGS BUT I NEED ONLY 1 THATS WHY I COMMNET THIS AND BELOW IS USING 1 PARAMETER
        //Task<IEnumerable<Patients>> FilterDataSearching(DateTime? fromDate, DateTime? toDate, string Patientname);
        Task<IEnumerable<Patients>> FilterDataSearching( string Patientname);
       
        Task<Doctors> Insert(Doctor emp);
         Task<Doctors> Update(Doctor emp);

        Task<List<Doctors>> GetAllDoctors();
       

        Task<bool> DoctorExists(int doctorId);
        Task<bool> InsertSlot(View_DoctorTimeSlot data);

        //To show TimeSlotDocor Data in Table
        Task<IEnumerable<View_DoctorTimeSlot>> GetDoctorsTimeSlot();
        Task<IEnumerable<View_PatientTimeSlot>> GetPatientsTimeSlot();
        Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData();
        Task<bool> UpdateSlot(View_DoctorTimeSlot data);



        //For updatation &  edit of patients table data below 2 s are
        Task<Patients> Update(Patient emp);
        Task<Patients> GetByIdPatient(int Id);

        //WORK -> TO UPDATE ISAPPROVED COLUMN VALUE IN THE DATABASE TABLE TIMESLOT_PATIENTS AND SHOW IN TABLE APPROVED OR REJECTED
        Task UpdateApprovalStatus(int id, bool isApproved);



    }
}
