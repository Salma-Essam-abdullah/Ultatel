.NET Web API CRUD Application

This repository contains a .NET Web API CRUD application built using an N-tier architecture. The application allows you to perform Create, Read, Update, and Delete operations on a database.

Technologies Used

    .NET 
    ASP.NET Core Web API
    Entity Framework Core
    SQL Server

Project Structure

The project follows an N-tier architecture with the following layers:

    Models: Contains the entity classes representing the data model.
    Data Access Layer: Contains the DbContext class for Entity Framework Core , Repository and Repository interfaces.
    Business Login Layer: Contains Dtos , Helpers(mapping,pagination) , Services , Services interfaces and implementations for business logic.
    Api: Contains API controllers for handling HTTP requests.

Setup Instructions

Follow these steps to set up the project on your local machine:

    Clone the repository:

    git clone https://github.com/Salma-Essam-abdullah/Ultatel/

Install dependencies:
Open the solution in Visual Studio and restore the NuGet packages.

Configure the database:

    Update the connection string in appsettings.json to point to your SQL Server instance.


Update the database:
Open the Package Manager Console in Visual Studio and run the following commands:
Update-Database

📝 NOTE: There is an endpoint to register admin so that 
Admin users VIEW LOGS OF ALL STUDENTS


# Deployment Overview

## Note

The actual deployment was not completed due to time constraints. However, the outlined steps demonstrate familiarity with the process and the benefits of deploying applications on cloud servers.

## Deploying .NET App, SQL Database, and Angular App on AWS

### Overview

This guide provides an overview of deploying a .NET application, SQL database, and Angular application on Amazon Web Services (AWS). This process ensures scalability and ease of upgrading and sets the foundation for future continuous integration/continuous deployment (CI/CD) pipelines.

### Steps to Deploy

1. *.NET Application Deployment*
    - *Create an EC2 Instance*: Launch an Amazon EC2 instance to host the .NET application.
    - *Install .NET Core Runtime*: Install the necessary .NET Core runtime on the EC2 instance.
    - *Deploy Application*: Deploy the .NET application to the EC2 instance using manual deployment via SSH.
    - *Set Up Load Balancer*: Use Amazon Elastic Load Balancing (ELB) to distribute incoming application traffic across multiple EC2 instances for high availability and fault tolerance.

2. *SQL Database Deployment*
    - *Create RDS Instance*: Set up an Amazon RDS instance with the SQL Server engine.
    - *Configure Security Groups*: Ensure proper security groups are configured to allow access from the .NET application.
    - *Database Migration*: Migrate existing databases to the RDS instance using database migration tools or backup and restore methods.

3. *Angular Application Deployment*
    - *Create S3 Bucket*: Create an Amazon S3 bucket to host the static files of the Angular application.
    - *Build Angular App*: Build the Angular application using the Angular CLI.
    - *Upload to S3*: Upload the built files to the S3 bucket.
    - *Configure CloudFront*: Set up Amazon CloudFront to serve the Angular application from the S3 bucket.

### Benefits of Cloud Deployment

- *Scalability*: AWS provides auto-scaling capabilities for both the EC2 instance hosting the .NET application and the RDS instance, ensuring the application can handle increased traffic seamlessly.
- *Cost Efficiency*: AWS's pay-as-you-go model ensures that you only pay for the resources you use, optimizing costs as the application scales.

### Future Plans: CI/CD Pipeline with Jenkins

- *Continuous Integration*: Integrating Jenkins will allow for automated builds and testing of the application, ensuring code quality and reducing integration issues.
- *Continuous Deployment*: Setting up a Jenkins pipeline will automate the deployment process, reduce manual intervention, and speed up the release cycle.
- *Scalability and Efficiency*: A CI/CD pipeline with Jenkins enhances scalability and operational efficiency by automating repetitive tasks, providing faster feedback, and allowing for quicker iteration on features and bug fixes.

### Conclusion

Deploying a .NET application, SQL database, and Angular application on AWS provides a robust, scalable, and cost-effective solution. Incorporating a CI/CD pipeline with Jenkins in the near future will further enhance the deployment process, ensuring continuous integration and deployment for better scalability and efficiency.
