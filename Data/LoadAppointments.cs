using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Appt_Scheduler.Models;

namespace Appt_Scheduler.Data
{

    public static class LoadPatients
    {
        public static void LoadDefaultPatients(DomainContext context)
        {
            // Make sure DB is created
            context.Database.EnsureCreated();

            // Seed patients if they don't already exist
            if (!context.Patients.Any())
            {
                var patients = new List<Patient>
                {
                    new Patient
                    {
                        FName = "Kelly",
                        LName = "Elder",
                        DOB = new DateOnly(1971, 5, 15),
                        Sex = "Female",
                        Phone = "406-555-1111",
                        Email = "kelder@gmail.com"
                    },
                    new Patient
                    {
                        FName = "Jane",
                        LName = "Carver",
                        DOB = new DateOnly(1990, 8, 22),
                        Sex = "Female",
                        Phone = "406-555-2222",
                        Email = "jane.carver@aol.com"
                    },
                    new Patient
                    {
                        FName = "Sam",
                        LName = "Taylor",
                        DOB = new DateOnly(2000, 2, 10),
                        Sex = "Male",
                        Phone = "406-986-4841",
                        Email = "sam.taylor@gmail.com"
                    }
                };

                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }

    public static class LoadAppointments
    {
        public static void LoadDoctorAvailability(DomainContext context)
        {

            // Make sure DB is created
            context.Database.EnsureCreated();


            // Seed doctors if they don't already exist
            if (!context.Doctors.Any())
            {
                var doctors = new List<Doctor>
                 {
                     new Doctor {  DoctorFullName = "Dr. Alice Smith", DEANumber = "DEA001", PhoneNumber = "555-1234", Email = "alice@example.com" },
                     new Doctor {  DoctorFullName = "Dr. Bob Jones", DEANumber = "DEA002", PhoneNumber = "555-5678", Email = "bob@example.com" },
                     new Doctor { DoctorFullName = "Dr. Carol Brown", DEANumber = "DEA003", PhoneNumber = "555-8765", Email = "carol@example.com" }
                 };


                context.Doctors.AddRange(doctors);
                context.SaveChanges();
            }

            var startDate = new DateOnly(2025, 5, 1);
            var endDate = new DateOnly(2025, 5, 31);
            var doctorIds = new[] { 1, 2, 3 };

            var availabilities = new List<DoctorAvailability>();

            foreach (var doctorId in doctorIds)
            {
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    // Only Monday to Friday
                    if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                        continue;

                    // Create slots from 8 AM to 5 PM (last slot starts at 4 PM)
                    for (int hour = 8; hour < 17; hour++)
                    {
                        var timeSlot = new TimeOnly(hour, 0);

                        var availability = new DoctorAvailability
                        {
                            AvailDate = date,
                            StartTime = timeSlot,
                            IsBooked = false,
                            DoctorId = doctorId
                        };

                        // Avoid duplicate seeding
                        bool exists = context.DoctorAvailabilities.Any(da =>
                            da.DoctorId == doctorId &&
                            da.AvailDate == date &&
                            da.StartTime == timeSlot);

                        if (!exists)
                            availabilities.Add(availability);
                    }
                }
            }

            if (availabilities.Any())
            {
                context.DoctorAvailabilities.AddRange(availabilities);
                context.SaveChanges();
            }
        }
    }
}
