using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Repository
{
    public class DoctorsRepo
    {
        
            private readonly AppDbContext appDbContext;

            public DoctorsRepo(AppDbContext appDbContext)
            {
                this.appDbContext = appDbContext;
            }
            //Shows List of Table data
            public async Task<List<Doctors>> GetAllDataDoctor()
            {
                try
                {
                    var data = appDbContext.Doctors.OrderByDescending(x=>x.Id).ToList();
                    return await Task.FromResult(data);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        //FOR EDIT AND DELETE 
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

        
        //INSERT IN DOCTOR
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
        //UPDATE IN DOCTOR
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
        //Used to show all doctor names in a dropdown in AddDoc view
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

        public async Task<IEnumerable<Doctors>> FilterDataSearching(string DoctorName)
        {
            var query = appDbContext.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(DoctorName))
            {
                query = query.Where(p => p.DoctorName.Contains(DoctorName));
            }



            return await query.ToListAsync();
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



        //To show TimeSlotDocor Data in Table
        public async Task<IEnumerable<View_DoctorTimeSlot>> GetDoctorsTimeSlot()
        {
            try
            {
                return await appDbContext.TimeSlotDoctor
                    .Join(appDbContext.Doctors, a => a.DoctorId, e => e.Id, (a, e) => new View_DoctorTimeSlot
                    {
                        Id = a.Id, // Include the Id property from TimeSlotDoctor
                        DoctorId = e.Id, // Include the DoctorId property from TimeSlotDoctor
                        DoctorName = e.DoctorName,
                        FromTime = a.FromTime,
                        ToTime = a.ToTime
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

    }







}

