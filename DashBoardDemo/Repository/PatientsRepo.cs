using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Repository
{
    public class PatientsRepo
    {
        private readonly AppDbContext appDbContext;

        public PatientsRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        
        }


        //Shows List of Table data of Patients
        public async Task<List<Patients>> GetAllData()
        {
            try
            {
                var data = appDbContext.Patients.ToList();
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //DONOT DELETE
        /* public async Task<IEnumerable<Patients>> FilterDataSearching(DateTime? fromDate, DateTime? toDate, string PatientName)
         {
             var query = appDbContext.Patients.AsQueryable();

             if (!string.IsNullOrEmpty(PatientName))
             {
                 query = query.Where(p => p.Name.Contains(PatientName));
             }

             if (fromDate.HasValue)
             {
                 query = query.Where(p => p.Dob >= fromDate.Value.Date);
             }

             if (toDate.HasValue)
             {
                 query = query.Where(p => p.Dob <= toDate.Value.Date);
             }

             return await query.ToListAsync();
         }*/
        //SEARCH BY PATIENT NAME
        public async Task<IEnumerable<Patients>> FilterDataSearching( string PatientName)
        {
            var query = appDbContext.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(PatientName))
            {
                query = query.Where(p => p.Name.Contains(PatientName));
            }

           

            return await query.ToListAsync();
        }


        //Delete
        public async Task<List<Patients>> Delete(int Id)
        {
            try
            {
                List<Patients> deps = appDbContext.Patients.Where(x => x.Id == Id).ToList();
                appDbContext.Patients.RemoveRange(deps);
                appDbContext.SaveChanges();
                return await Task.FromResult(deps);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //UPDATE FOR PATIENT TABLE  DATA
        public async Task<Patients> Update(Patient add)
        {
            try
            {
                var empData = appDbContext.Patients.Where(x => x.Id == add.Id).FirstOrDefault();

               
                    empData.Name = add.Name;
                    empData.Dob = add.Dob;
                    empData.Phone = add.Phone;
                    empData.Email = add.Email;

                    appDbContext.Patients.Update(empData);
                    appDbContext.SaveChanges();
                

                return await Task.FromResult(empData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //  USED FOR SEND DATA FROM PATIENTVIEW TO THE EDIT FORM FOR EDIT PURPOSE
        public async Task<Patients> GetByIdPatient(int Id)
        {
            try
            {

                var data = appDbContext.Patients.Single(x => x.Id == Id);
                return await Task.FromResult(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
