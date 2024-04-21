using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.ServerSideRepository
{
    public class ServerSide_PatientsTimeSlotRepo
    {
        private readonly AppDbContext appDbContext;

        public ServerSide_PatientsTimeSlotRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<DataTableResponse<View_PatientTimeSlot>> GetPatientsSlotTime(DataTableRequest dataTableRequest, string sortColumn, string sortColumnDirection)
        {
            var query = from ft in appDbContext.TimeSlot_Patients
                        join t in appDbContext.Patients on ft.PatientId equals t.Id
                        join d in appDbContext.Doctors on ft.DoctorId equals d.Id
                        select new View_PatientTimeSlot
                        {
                            Id = ft.Id,
                            PatientId = ft.PatientId,
                            Name = t.Name,
                            Date = ft.Date,
                            SlotTime = ft.SlotTime,
                            IsApproved = ft.IsApproved,
                            DoctorName = d.DoctorName,
                            DoctorId = d.Id
                        };

            var result = await query.ToListAsync();


            // Apply search filter
            if (!string.IsNullOrEmpty(dataTableRequest.Search.Value))
            {
                string searchValue = dataTableRequest.Search.Value.Trim().ToLower();
                query = query.Where(x => x.Id.ToString().Contains(searchValue) ||
                                         x.PatientId.ToString().Contains(searchValue) ||
                                         x.DoctorId.ToString().Contains(searchValue) ||
                                         x.Name.ToLower().Contains(searchValue) ||
                                          x.DoctorName.ToLower().Contains(searchValue) ||
                                         x.Date.ToString().Contains(searchValue) ||
                                         x.SlotTime.ToString().Contains(searchValue) ||
                                         x.IsApproved.ToString().Contains(searchValue));
            }

            // Apply sorting
            switch (sortColumn)
            {
                case "Name":
                    query = sortColumnDirection == "asc" ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                    break;
                case "DoctorName":
                    query = sortColumnDirection == "asc" ? query.OrderBy(x => x.DoctorName) : query.OrderByDescending(x => x.DoctorName);
                    break;
                case "Date":
                    query = sortColumnDirection == "asc" ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date);
                    break;
                case "SlotTime":
                    query = sortColumnDirection == "asc" ? query.OrderBy(x => x.SlotTime) : query.OrderByDescending(x => x.SlotTime);
                    break;
                // Add more cases for other columns if needed
                default:
                    break;
            }

            int totalRecords = await query.CountAsync();

            // Paginate the data
            var pagedData = await query.Skip(dataTableRequest.Start).Take(dataTableRequest.Length).ToListAsync();

            var dataTableResponse = new DataTableResponse<View_PatientTimeSlot>
            {
                Draw = dataTableRequest.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = pagedData
            };

            return dataTableResponse;
        }

    }
}

