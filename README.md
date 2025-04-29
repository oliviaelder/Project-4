# Project 4: Appiontment Scheduler
Challenge: Build an appointment scheduling system.

Context: For the appointment scheduler project, I developed a system for users to schedule, edit, and cancel appointments. The application needed to handle appointment time slots, user data, and conflict resolution.

Action: I created models for users, appointments, and time slots. The system allowed users to book available time slots and would flag conflicts if users tried to book overlapping appointments. The database schema included relationships between users and appointments, ensuring that each user could have multiple appointments.

Results: The main challenge was ensuring the availability of time slots and properly handling overlapping appointment requests. After testing and iterating, I was able to implement a system where users could only book open slots and received proper error messages for conflicts.

Reflection: This project gave me insight into real-time applications and handling conflicts in scheduling systems. It was rewarding to see users book appointments seamlessly.


## Live Demo Limitations

- Visual Studio 2022 or higher
- SQL Server (LocalDB or a similar setup) for database support
- .NET Core SDK (or .NET 5/6 depending on the version you're using)

## Setup

1. Clone the repository to your local machine.
   
   `bash
   git clone https://github.com/oliviaelder/Project-4.git
   cd Project-4
   
4. Open the solution file Appt_Scheduler.sln in Visual Studio
5. Build the solution
6. Run the application in Visual Studio.  The project will automatically create a local database upon first run.  It will also create sample Doctor and Patient data, as well as seed the DoctorAvailability table with 9 hourly appointments every day in May for the sample Doctors. 

## Project can not be run directly from GitHub
This is an ASP.NET MVC application that requires a local server (like IIS Express) and a local database to run. Unfortunately, it cannot be directly hosted on GitHub Pages or any other platform that doesn't support .NET or database operations.
