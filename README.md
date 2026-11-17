# DogCenter

DogCenter is an ASP.NET Core MVC web application designed for a dog training and care center offering diverse services.

Live demo: https://dogcenter-d8dkd7abebb8f2g4.swedencentral-01.azurewebsites.net (The SQL instance is free, so the first load may take a bit longer.)

## Project overview and purpose

DogCenter is a centralized system that combines a public-facing website with an internal management application, allowing visitors to explore the service offerings of the center and authenticated users to manage the information about the owners and dogs using those services, including the dogs’ medical history.

The current version is the foundation for a more elaborate management system and offers the following functionalities:

- Visitors can browse the company home page, about page, and service categories.
- Authenticated users can manage owner records.
- Authenticated users can manage dog profiles and link them to owners.
- Authenticated users can add, edit, and delete medical records for individual dogs.
- Authenticated users can create, edit, and delete services in the service catalog.

The Dog entity is central to the application. Each dog has an owner and can have multiple medical records. An owner can have multiple dogs.

This Fundamentals version focuses on database management, CRUD operations, validation, authentication, and the MVC architecture.

Future Development:DogCenter is intended to evolve into a more comprehensive management application that will allow both customers and center personnel to manage various aspects of the center's operations, including scheduling, bookings, and attendance tracking.

## Technologies and architecture

This project uses the following technologies:

- ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- SQL Server provider 
- ASP.NET Core Identity 
- .NET 10 

The application is organized into the following areas:

- Controllers – Handle user requests and determine which actions the application should perform.
- Services – Handle the logic behind the actions, including retrieving and updating information in the database.
- Data – Contains the database context, which connects the application to the database, and the migrations used to create and update its structure, including seeding sample data.
- Models – Define the data stored in the database and used by different pages.
- Views – Contain the Razor pages that control what users see and allow them to interact with the application.
- Areas/Identity/Pages – Contain the built-in ASP.NET Core Identity pages for registration, login, and account management.

## Entity models and relationships

The data model is built around dogs and their owners.

- Owner – Represents a customer of the dog center. An owner can have multiple dogs, creating a one-to-many relationship.
- Dog – Represents a dog registered at the center. Each dog is linked to an owner through the OwnerId foreign key and can have multiple medical records.
- MedicalRecord – Stores information about vaccinations, treatments, and other medical events. Each record is linked to a dog through the DogId foreign key. The next due date is optional.
- Service – Represents a service offered by the center, including its category, price, and description. The price uses decimal precision (5,2). Services are currently independent of the other entities.

The actual relationship setup in ApplicationDbContext is:

`Owner 1 --- * Dog
Dog   1 --- * MedicalRecord
Service is independent of the other entities.`

The project also seeds sample data.

## Authentication and authorization

DogCenter uses ASP.NET Core Identity for user registration, login, and account management. User information is stored in the SQL database. Since no SMTP configuration is provided, the registration confirmation is handled by the default Identity workflow, which lets the user confirm their account on the website after registering.

The [Authorize] attribute restricts access to owner, dog, and medical record management, as well as service creation, editing, and deletion. Public visitors can browse the website and service catalog without registering.

Current limitation: Public registration has been left enabled to facilitate testing and evaluation of the application. This version does not distinguish between regular registered users and administrators, so any authenticated user can modify management records. This is subject to change in the future.

## Database setup and migrations

The ApplicationDbContext inherits from IdentityDbContext, allowing both application data and user authentication information to be stored in the same database.

A total of 4 migrations are used to create and update the database structure. The migrations also seed sample data, allowing the application's functionality to be tested immediately after setup.

Note: The database connection is configured through ConnectionStrings:DefaultConnection. Database credentials are not included in the repository and must be configured locally.

## Running the application

### Prerequisites

- .NET SDK 10 installed
- A valid SQL Server instance accessible from the application

### Configure the database connection

The application uses ConnectionStrings:DefaultConnection. The connection string must be configured for the local SQL server instance, in the User Secrets.

Example:

`{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DogCenter;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}`

### Application Initialization

Restore the NuGet packages from the NuGet package manager if this has not been done automatically.

Build the solution.

### Apply EF Core migrations

Apply the existing migrations using Package Manager Console (update-database) or the .NET CLI from the project directory(dotnet ef database update). This creates the database structure, and inserts the sample data.

### Run the app

After a successful build and applied migrations, run the application. The home page will load and the service catalog will be available for browsing without login.

To access the management functionality, register a new account and confirm it immediately after registration using the default ASP.NET Core Identity workflow.

Once logged in, the Owners and Dogs pages become available, along with Medical Records on the Dog Details page. Service management is accessible through the Add Service button on the Service Categories page and the Edit/Delete buttons on individual Service Details pages.

Authenticated users can perform full CRUD operations on Dogs, Medical Records, and Services. Owner records can be created, viewed, and edited. 

### Love your dog