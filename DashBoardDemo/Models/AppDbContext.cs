using DashBoardDemo.ModelDb;
using Microsoft.EntityFrameworkCore;
using System;

namespace DashBoardDemo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<TimeSlot_Patients> TimeSlot_Patients { get; set; }
        public DbSet<TimeSlotDoctor> TimeSlotDoctor { get; set; }

        public DbSet<View_DoctorTimeSlot> View_DoctorTimeSlot { get; set; }
        public DbSet<View_PatientTimeSlot> View_PatientTimeSlot { get; set; }
        public DbSet<patientLogin> patientLogin { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLogin>(entity => {
                entity.HasKey(k => k.id);
            });
        }
    }
}
