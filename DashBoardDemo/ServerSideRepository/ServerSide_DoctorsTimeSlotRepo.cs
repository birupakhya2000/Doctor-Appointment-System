using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.ServerSideRepository
{
    public class ServerSide_DoctorsTimeSlotRepo
    {
        private readonly AppDbContext appDbContext;

        public ServerSide_DoctorsTimeSlotRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<DataTableResponse<View_DoctorTimeSlot>> GetDoctorsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection)
        {
            var query = from ft in appDbContext.TimeSlotDoctor
                        join t in appDbContext.Doctors on ft.DoctorId equals t.Id
                        select new View_DoctorTimeSlot
                        {

                            Id = ft.Id, 
                            DoctorId = ft.DoctorId, 
                            DoctorName = t.DoctorName,
                            FromTime = ft.FromTime,
                            ToTime = ft.ToTime
                        };

            // Apply search filter
            if (!string.IsNullOrEmpty(dataTableRequest.Search.Value))
            {

                string searchValue = dataTableRequest.Search.Value.Trim().ToLower();
                query = query.Where(x => x.Id.ToString().Contains(searchValue) ||
                                         x.DoctorId.ToString().Contains(searchValue) ||
                                         x.DoctorName.Contains(searchValue) ||
                                         x.FromTime.ToString().Contains(searchValue) ||
                                          x.ToTime.ToString().Contains(searchValue));
                                        
            }
            // Apply sorting
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                switch (sortColumn)
                {
                    case "DoctorName":
                        query = sortColumnDirection == "asc" ? query.OrderBy(x => x.DoctorName) : query.OrderByDescending(x => x.DoctorName);
                        break;
                    case "FromTime":
                        query = sortColumnDirection == "asc" ? query.OrderBy(x => x.FromTime) : query.OrderByDescending(x => x.FromTime);
                        break;
                    case "ToTime":
                        query = sortColumnDirection == "asc" ? query.OrderBy(x => x.ToTime) : query.OrderByDescending(x => x.ToTime);
                        break;
                    // Add more cases for other columns if needed
                    default:
                        break;
                }

            }

            int totalRecords = await query.CountAsync();

            // Get total records count
            

           


            // Paginate the data
            var pagedData = await query.Skip(dataTableRequest.Start).Take(dataTableRequest.Length).ToListAsync();

            var dataTableResponse = new DataTableResponse<View_DoctorTimeSlot>
            {
                Draw = dataTableRequest.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = pagedData
            };

            return dataTableResponse;
        }

        public async Task<List<Doctors>> GetAllDoctors()
        {
            try
            {
                var data = await appDbContext.Doctors.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //For insert data into the TimeSlotDoctor Table by GetBy Id
        public async Task<Doctors> GetByIdDoc(int id)
        {
            try
            {
                var data = await appDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //For Insert data into  TimeSlotDoctor  table
        public async Task InsertSlot(View_DoctorTimeSlot data)
        {
            try
            {
                var timeSlotDoctor = new TimeSlotDoctor()
                {
                    DoctorId = data.DoctorId,
                    FromTime = data.FromTime,
                    ToTime = data.ToTime
                };

                appDbContext.TimeSlotDoctor.Add(timeSlotDoctor);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }

        public async Task UpdateSlot(View_DoctorTimeSlot data)
        {
            try
            {
                var existingSlot = await appDbContext.TimeSlotDoctor.FirstOrDefaultAsync(t => t.Id == data.Id);
                if (existingSlot != null)
                {
                    existingSlot.FromTime = data.FromTime;
                    existingSlot.ToTime = data.ToTime;
                    await appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw;
            }
        }


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
