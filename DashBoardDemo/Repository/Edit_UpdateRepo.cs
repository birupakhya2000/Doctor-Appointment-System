using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Repository
{
    public class Edit_UpdateRepo
    {

        private readonly AppDbContext appDbContext;

        public Edit_UpdateRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        /* public async Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData()
         {
             try
             {
                 var data = (from et in appDbContext.TimeSlotDoctor
                             join em in appDbContext.Doctors on et.DoctorId equals em.Id

                             select new View_DoctorTimeSlot
                             {
                                 DoctorId = em.Id,
                                 Id = et.Id,
                                 DoctorName = em.DoctorName,
                                 FromTime = et.FromTime,
                                 ToTime = et.ToTime
                             }).ToList();

                 return await Task.FromResult(data);
             }
             catch ( Exception ex)
             {

                 throw;
             }*/

        //Try
        public async Task<IEnumerable<View_DoctorTimeSlot>> GetPutDocTimeSlotData()
        {
            var data = await (from et in appDbContext.TimeSlotDoctor
                              join em in appDbContext.Doctors on et.DoctorId equals em.Id
                              select new View_DoctorTimeSlot
                              {
                                  DoctorId = em.Id,
                                  Id = et.Id,
                                  DoctorName = em.DoctorName,
                                  FromTime = et.FromTime,
                                  ToTime = et.ToTime
                              }).ToListAsync();

            return data;
        }

        


        
    }
}

    

