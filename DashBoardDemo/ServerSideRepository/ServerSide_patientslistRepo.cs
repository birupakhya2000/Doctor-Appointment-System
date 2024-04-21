using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;

namespace DashBoardDemo.ServerSideRepository
{
    public class ServerSide_patientslistRepo
    {
        private readonly AppDbContext appDbContext;

        public ServerSide_patientslistRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Patients> GetPatientsList(string searchValue, string sortColumn, string sortColumnDirection)
        {
            var data = appDbContext.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.Trim();
                data = data.Where(x => x.Id.ToString().Contains(searchValue) ||
                                       x.Name.Contains(searchValue) ||
                                       x.Dob.ToString().Contains(searchValue) ||
                                       x.Phone.Contains(searchValue) ||
                                       x.Email.Contains(searchValue));

            }
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                switch (sortColumn)
                {
                    case "Name":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Name) : data.OrderByDescending(x => x.Name);
                        break;
                    case "Dob":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Dob) : data.OrderByDescending(x => x.Dob);
                        break;
                    case "Phone":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Phone) : data.OrderByDescending(x => x.Phone);
                        break;
                    case "Email":
                        data = sortColumnDirection == "asc" ? data.OrderBy(x => x.Email) : data.OrderByDescending(x => x.Email);
                        break;
                    default:
                        break;
                }
            }

            return data;
        }

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

    }
}
