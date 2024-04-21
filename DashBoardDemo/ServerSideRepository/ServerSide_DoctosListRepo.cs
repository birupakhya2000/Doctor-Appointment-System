using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.ModelServerSide;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DashBoardDemo.ServerSideRepository
{
    public class ServerSide_DoctosListRepo
    {
        private readonly AppDbContext appDbContext;

        public ServerSide_DoctosListRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Doctors> GetDoctorList(string searchValue, string sortColumn, string sortColumnDirection)
        {
            var data = appDbContext.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.Trim();
                data = data.Where(x => x.Id.ToString().Contains(searchValue) || 
                                       x.DoctorName.Contains(searchValue) ||
                                       x.Phone.Contains(searchValue) ||
                                       x.Email.Contains(searchValue) ||
                                       x.Speciality.Contains(searchValue));
                                      
            }
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                switch (sortColumn)
                {
                    case "DoctorName":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.DoctorName) : data.OrderByDescending(x => x.DoctorName);
                        break;
                    case "Phone":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Phone) : data.OrderByDescending(x => x.Phone);
                        break;
                    case "Email":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Email) : data.OrderByDescending(x => x.Email);
                        break;
                    case "Speciality":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Speciality) : data.OrderByDescending(x => x.Speciality);
                        break;
                    default:
                        break;
                }
            }

            return data;
        }
        public async Task<Doctors> GetById(int Id)
        {
            try
            {

                var data = appDbContext.Doctors.Single(x => x.Id == Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<Doctors> Insert(Doctor add)
        {
            try
            {
                Doctors emp = new Doctors();
                emp.DoctorName = add.DoctorName;

                emp.Phone = add.Phone;
                emp.Email = add.Email;
                emp.Speciality = add.Speciality;



                appDbContext.Doctors.Add(emp);
                appDbContext.SaveChanges();
                return await Task.FromResult(emp);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Doctors> Update(Doctor add)
        {
            try
            {
                var empData = appDbContext.Doctors.Where(x => x.Id == add.Id).FirstOrDefault();

                empData.DoctorName = add.DoctorName;

                empData.Phone = add.Phone;
                empData.Email = add.Email;
                empData.Speciality = add.Speciality;

                appDbContext.Doctors.Update(empData);
                appDbContext.SaveChanges();
                return await Task.FromResult(empData);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        //Search
        public async Task<DataTableResponse<Doctors>> FilterDataSearching(string doctorName, DataTableRequest request, string searchValue, string sortColumn, string sortColumnDirection)
        {
            var data = appDbContext.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(doctorName))
            {
                searchValue = doctorName.Trim();
                data = data.Where(x => x.DoctorName.Contains(searchValue));
            }

            int totalRecords = await data.CountAsync();

            Console.WriteLine($"Total Records: {totalRecords}");

            var pagedData = await data.Skip(request.Start).Take(request.Length).ToListAsync();

            Console.WriteLine($"Paged Data Count: {pagedData.Count}");

            var dataTableResponse = new DataTableResponse<Doctors>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = pagedData
            };

            return dataTableResponse;
        }






    }
}
