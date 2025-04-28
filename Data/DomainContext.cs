using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using System.Collections;
using System.Net.Sockets;
using Appt_Scheduler.Models;

namespace Appt_Scheduler.Data
{
    public class DomainContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = AppointmentsDB");
            //    .LogTo(Console.WriteLine,
            //        new[] { DbLoggerCategory.Database.Command.Name },
            //        Microsoft.Extensions.Logging.LogLevel.Information)
            //.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Patient 1-to-Many Appointment Relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .IsRequired(true);  // An Appointment must have a Customer; 

            // Doctor 1-to-Many Appointment Relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(true);  // An Appointment must have a Doctor; 

            // Doctor 1-to-Many DoctorAvailability Relationship
            modelBuilder.Entity<DoctorAvailability>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.DoctorAvailabilities)
                .HasForeignKey(a => a.DoctorId)
                .IsRequired(true);  // An Appointment must have a Doctor; 


            //Create Doctors data
            //modelBuilder.Entity<Doctor>().HasData(new Doctor
            //{
            //    DoctorId = 1,  // Primary Key
            //    DoctorFullName = "Dr. John Doe",
            //    DEANumber = "DEA1234567",
            //    PhoneNumber = "555-123-4567",
            //    Email = "johndoe@example.com"
            //}
            // );

          //Pre-populate DoctorAvailability table with all appt times on a day
            //modelBuilder.Entity<DoctorAvailability>().HasData(
            //    new DoctorAvailability
            //    {
            //        AvailId = 1,  // Primary Key
            //        AvailDate = new DateOnly(2025, 4, 1),
            //        StartTime = new TimeOnly(9, 0),  // Example: 9:00 AM
            //        IsBooked = false,
            //        DoctorId = 1  // Assuming Doctor with ID 1 exists
            //    }
            //);
            //modelBuilder.Entity<Doctor>().HasData(new DoctorAvailability
            //{
            //    AvailId = 2,
            //    DoctorId = 1,
            //    AvailDate = new DateOnly(2025, 4, 1),
            //    StartTime = new TimeOnly(9, 0),  // Example: 8:00 AM
            //    IsBooked = false
            //});


        }
    }
}