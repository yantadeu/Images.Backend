# Image Management API

This repository contains a .NET API for managing images. It is built using .NET 8 and follows a clean architecture pattern. The project leverages various technologies and design patterns to ensure a robust and scalable solution.

## Features

- **.NET 8:** This project is built on .NET 8.

- **Clean Architecture:** The codebase follows the principles of clean architecture, separating concerns into distinct layers to achieve maintainability and testability.

- **CQRS and Mediatr:** Command Query Responsibility Segregation (CQRS) and Mediatr are used to separate the write and read operations, making the application more scalable and flexible.

- **Authentication:** The API utilizes .NET Core Identity and JWT Tokens for user authentication and authorization.

- **Logging:** Serilog is employed for comprehensive logging, allowing you to monitor and troubleshoot the application effectively.

- **Docker SQL Container:** The project includes a Docker SQL container for easy database setup and management.

- **API Versioning:** API versioning is implemented to ensure backward compatibility and smooth updates.

- **Fluent Validations:** FluentValidations are used for input validation, ensuring data integrity and security.

- **CQRS IPipelineBehavior:** Custom pipeline behaviors are employed to handle cross-cutting concerns such as validation, logging, and error handling in the CQRS pipeline.

## Getting Started

To get started with this project, follow these steps:

1.  Clone the repository to your local machine:

    bash

1.  `git clone https://github.com/yantadeu/Images.Backend.git`

2.  Open the solution in your preferred IDE

3. Configure Application:

** Provide efemeral database: 
- Network: docker network create upik_net
- Database: docker-compose -f docker-compose-db.yaml up -d
*User for login in api and authorize bearer token:*
- username: admin
- password: P@ssw0rd@123

  ** Run Api With Docker:
- Application: docker-compose up --build -d
- Access Application in 'http://localhost:8080/swagger/index.html'

  ** Run Api Without Docker:
- Create your postgres database or use provided database
- If you create your own database, configure your database connection in the `appsettings.json` file and update this using `dotnet ef database update`
- Run Application and access in 'http://localhost:8080/swagger/index.html'


## API Endpoints

Here are some of the key API endpoints:

- **POST /api/v1/images:** Create a new image.
- **GET /api/v1/images/{id}:** Retrieve a specific image by ID.
- **GET /api/v1/images:** Retrieve a list of image.
- **GET /api/v1/images/download:** Download a image.
- **DELETE /api/v1/images/{id}:** Delete a image.

- **POST /api/v1/auth/register:** Register a new user.
- **POST /api/v1/auth/login:** login endpoint.

