# Project 4: Appiontment Scheduler
Challenge: Build an appointment scheduling system.

Context: I created a backend web service using .NET MVC and Entity Framework to allow users to book, view, update, and cancel appointments. The focus was on using RESTful APIs to manage appointments and ensure no time conflicts.

Action: I built a .NET Web API project and created models for Doctor, Patient, Appointment, and DoctorAvailabilities. I used Entity Framework to connect to a SQL Server database. In the controller, I created endpoints for adding, updating, deleting, and checking available appointments.  

Result: The biggest challenge was correctly handling overlapping appointments. After improving the validation in the API, the scheduler reliably allowed only open time slots to be booked.

Reflection: This project helped me understand how to design a clean REST API, create and manage a database and its relationships with Entity Framework, and handle real-world validation problems like scheduling conflicts.


<br>
 <hr>
 <img src="https://github.com/oliviaelder/Project-4/raw/main/PatientFunctions.png" alt="Patient Functions" style="max-width: 100%; height: auto;">

<br>
 <hr>

  <img src="https://github.com/oliviaelder/Project-4/raw/main/DoctorFunctions.png" alt="Doctor Functions" style="max-width: 100%; height: auto;">
  
<br>
<hr>
   <img src="https://github.com/oliviaelder/Project-4/raw/main/Admin.png" alt="Admin Functions" style="max-width: 100%; height: auto;">
   
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
