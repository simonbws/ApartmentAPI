# Apartment Management API & Web Application

## Project Overview
This project includes an API and a Web Application for managing apartments and users.  
The backend uses ASP.NET Core, with Microsoft Identity and JWT (JSON Web Tokens) for user authentication and authorization.

The goal is to show a complete CRUD system for apartments, with user registration, login, and role management (Admin and Customer).

---

## Main Features

### API
- Create, read, update, and delete apartments and apartment numbers
- Register users with roles (Admin, Customer)
- Login and generate JWT tokens
- Role management using RoleManager
- Authorization with JWT and roles
- Pagination
- API versioning

### Web Application
- Display lists of apartments and apartment numbers
- Create, edit, and delete apartments using forms
- Register and log in users
- Role-based access to views

### Utility
- Static details helper file

---

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- Microsoft Identity
- JWT (JSON Web Tokens)
- AutoMapper
- Newtonsoft.Json
- SQL Server
- Bootstrap 5

---

## Design and Best Practices
- Repository Pattern – separates data access logic from controllers
- DTO (Data Transfer Objects) – safe way to pass data between API and frontend
- Razor Pages / MVC – simple UI with data validation
- AutoMapper – mapping between models and DTOs to simplify code

---

## Project Structure
- Apartment_API/ – backend API project  
  - Controllers/  
  - Models/  
  - Data/  
  - Repository/  

- Apartment_Web/ – frontend web application  
  - Controllers/  
  - Data/  
  - Repository/  
  - Models/  
  - Views/  

---
## How to Run the Project

1. Clone the repository : Clone the repository to your local computer. 
2. Database configuration: In the `appsettings.json` file in the API folder, set the database connection in `ConnectionStrings:DefaultConnection`. Ensure that the SQL server is running and the database is accessible.    
3. Performing migration: In the terminal in the API directory, run `dotnet ef database update` to create all tables, including Identity, roles, and users in the database.  
4. Launch: Set up the API and WebApp starter projects. The backend will be ready to handle requests from WebApp, Swagger, or Postman.    
5. Registration and login: Register a user with the `Admin` or `Customer` role. When using the API via Postman, a JWT token is generated during login, which must be used in the `Authorisation: Bearer <token>` header when accessing protected API endpoints.
---
## Based on
Author: simonbws
This project was inspired by an online Udemy course. 
[View the Certificate for this Course] https://www.udemy.com/certificate/UC-76d41f5a-d687-4275-8aca-06352d3278dd/
### Screenshots:

<img width="1039" height="939" alt="image" src="https://github.com/user-attachments/assets/b833cf74-7739-42df-bc9f-7155dfc62764" />
<img width="1044" height="701" alt="image" src="https://github.com/user-attachments/assets/3edfb45f-b976-4bbd-99c2-bf0b807765ad" />
<img width="1795" height="1025" alt="image" src="https://github.com/user-attachments/assets/7fdfef76-a899-4be5-90db-f52cd448d1c0" />
<img width="995" height="593" alt="image" src="https://github.com/user-attachments/assets/19dd19f7-41b1-4b4d-b299-4577d3525094" />
<img width="1391" height="589" alt="image" src="https://github.com/user-attachments/assets/6d57c1cb-991f-4b80-bda9-28675fdeb65b" />

### Creating apartment number

<img width="1954" height="934" alt="image" src="https://github.com/user-attachments/assets/93396ff2-b57d-4c48-bb0b-1d54f81d8be3" />
<img width="1047" height="698" alt="image" src="https://github.com/user-attachments/assets/dcb528ed-c8c3-4462-a136-371f9e019cbf" />
<img width="1198" height="508" alt="image" src="https://github.com/user-attachments/assets/70e6ae22-5842-46bf-9875-7d75811ea590" />

### Postman testing

<img width="811" height="910" alt="image" src="https://github.com/user-attachments/assets/277417e0-188e-43ce-b516-28b457f0a2aa" />
<img width="904" height="792" alt="image" src="https://github.com/user-attachments/assets/a3c42b7d-04da-4d58-9c80-4b50879556fd" />

