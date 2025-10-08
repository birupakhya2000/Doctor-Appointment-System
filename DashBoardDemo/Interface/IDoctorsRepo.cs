using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IDoctorsRepo
    {
        Task<List<Doctors>> GetAllDataDoctor();
        Task<Doctors> GetById(int Id);
        Task<Doctors> Insert(Doctor add);
        Task<Doctors> Update(Doctor add);
        Task<List<Doctors>> GetAllDoctors();
        Task<Doctors> GetByIdDoc(int id);
        Task<IEnumerable<Doctors>> FilterDataSearching(string DoctorName);
        Task InsertSlot(View_DoctorTimeSlot data);
        Task UpdateSlot(View_DoctorTimeSlot data);
        Task<IEnumerable<View_DoctorTimeSlot>> GetDoctorsTimeSlot();
    }
}