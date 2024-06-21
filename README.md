.NET Web API CRUD Application

This repository contains a .NET Web API CRUD application built using an N-tier architecture. The application allows you to perform Create, Read, Update, and Delete operations on a database.
Table of Contents

    Technologies Used
    Project Structure
    Setup Instructions
    Running the Application

Technologies Used

    .NET 
    ASP.NET Core Web API
    Entity Framework Core
    SQL Server

Project Structure

The project follows an N-tier architecture with the following layers:

    Models: Contains the entity classes representing the data model.
    Data: Contains the DbContext class for Entity Framework Core.
    Repositories: Contains repository interfaces and implementations for data access.
    Services: Contains service interfaces and implementations for business logic.
    Controllers: Contains API controllers for handling HTTP requests.

Setup Instructions

Follow these steps to set up the project on your local machine:

    Clone the repository:

    git clone https://github.com/your-username/https://github.com/Salma-Essam-abdullah/Ultatel/

Install dependencies:
Open the solution in Visual Studio and restore the NuGet packages.

Configure the database:

    Update the connection string in appsettings.json to point to your SQL Server instance.


Update the database:
Open the Package Manager Console in Visual Studio and run the following commands:
Update-Database
