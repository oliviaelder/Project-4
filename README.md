# Project 4: Appiontment Scheduler

This project is an ASP.NET MVC web application that allows users to [briefly describe what your project does]. It uses Entity Framework for database interactions, and a local database is created automatically when the application is run for the first time.

## Requirements

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
