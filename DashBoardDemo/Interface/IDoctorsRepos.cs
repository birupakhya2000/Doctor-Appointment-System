using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface IDoctorsRepos
    {
        Task<List<Doctors>> GetAllDataDoctor();
        Task<Doctors> GetById(int Id);
        List<string> GetDoctorTimeSlots(int DoctorId, DateTime selectedDate);
    }
}