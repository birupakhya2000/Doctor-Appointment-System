using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.Interface
{
    public interface IPatientsService
    {
        
        //Task<Patients> Update(Patient add);
       
        Task<Patients> GetByIdFor(int Id);
        Task<IEnumerable<Patients>> PutPatientvalue(int Id);
        Task<Patients> GetPatientById(int Id);


        Task AddTimeSlot(int patientId, TimeSlot_Patients timeSlot_Patients);

        //INSERT DATA FROM THE FROM INTO PATIENTS TABLE AND VIEW_PATIENTSTIMESLOT TABLE
        /*Task InsertPatientAndTimeSlot(Patient patientViewModel, View_PatientTimeSlot timeSlotViewModel);*/

    }
}
