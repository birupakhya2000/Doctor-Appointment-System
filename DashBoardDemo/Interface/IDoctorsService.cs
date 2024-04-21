using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.Interface
{
    public interface IDoctorsService
    {
        Task<List<Doctors>> GetAllDataDoctor();
        Task<Doctors> GetById(int Id);


        //Task<List<string>> GetAllDoctorTimeSlots(int doctorId, DateTime fromTime);
        List<string> GetDoctorTimeSlots(int DoctorId, DateTime selectedDate);

    }
}
