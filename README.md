
#  Password Manager API

This backend API, built with C# .NET 6, provides endpoints for managing encrypted passwords. The API communicates with a SQL Server database, which is hosted in a Docker container. The passwords are stored encrypted using Base64 encoding.


## Tech Stack

- **.NET 7**: Framework for building the web API.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database system, hosted in a Docker container.
- **Swashbuckle.AspNetCore**: For API documentation with Swagger.

## Setup Instructions

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop) (for SQL Server container)
- [Visual Studio]([https://visualstudio.microsoft.com/]) or another preferred code editor

### Clone the Repository

```bash
git clone <repository-url>
cd PasswordManager
```
### 2. Set Up SQL Server with Docker
Docker-compose.yml file containes the SQL Server docker information.

```bash
docker-compose up
```
### 3. Create Database and Table 
 Connect the SQL Server in visual studio and Run the PasswordManagerTableCreate.sql script to create new database and password.
 
#### 4. Set Up Database

- **Update Connection String:** Modify appsettings.development.json to include your SQL Server connection 

```json
 "ConnectionStrings": {
   "ConnectionString": "Data Source=localhost,1433;Initial Catalog=PersonalPasswordManager;User ID=sa;Password=Passw0rd!Manager;Trust Server Certificate=True"
 }
```

## API Reference

#### Get all Password

```http
  GET /api/PasswordManager/GetAllPassword
```
**Description:** Retrieve all encrypted passwords

**Response**
```Json
[
  {
    "id": 1,
    "category": "work",
    "app": "outlook",
    "userName": "testuser@mytest.com",
    "encryptedPassword": "TXlQYXNzd29yZEAxMjM="
  },
  {
    "id": 2,
    "category": "school",
    "app": "messenger",
    "userName": "testuser@mytest.com",
    "encryptedPassword": "TmV3UGFzc3dvcmRAMTIz"
  }
]

```

#### Get Password by Id

```http
  GET /api/PasswordManager/GetPasswordById?id=2&decrypt=true
```
**Description:** Retrive the particular password record by Id

**Response**
```Json
{
  "statusCode": "SUCCESS",
  "statusText": "SUCCESS",
  "data": {
    "passwordManagerId": 2,
    "category": "work",
    "app": "Gmail",
    "userName": "testuser2123@gmail.com",
    "decryptedPassword": "Test@123$",
    "encryptedPassword": "noqrB0KmTowj3abrkTzNIQ=="
  }
}
```

#### Add new Password to the manager

```http
  POST /api/PasswordManager/AddPassword
```
**Description:** Add a new password record in the manager

**Request**
```Json
{
  "category": "work",
  "app": "outlook",
  "userName": "testuser@mytest.com",
  "decryptedpassword": "MyPassword123"
}
```
**Response**
```Json
{
  "statusCode": "SUCCESS",
  "statusText": "Password Added Successfully",
  "data": 3
}

```
#### Update the existing Password 

```http
  PUT /api/PasswordManager/UpdatePassword
```
**Description:** Update the existing password record in the manager

**Request**
```Json
{
  "passwordManagerId":3
  "category": "work",
  "app": "outlook",
  "userName": "testuser@mytest.com",
  "decryptedpassword": "MyPassword1234"
}
```
**Response**
```Json
{
  "statusCode": "SUCCESS",
  "statusText": "Password Updated Successfully",
  "data": 3
}

```

#### Delete Password by Id

```http
  GET /api/PasswordManager/DeletePassword?id=2
```
**Description:** Delete the password from the table . It is an irreversible process

**Response**
```Json
{
  "statusCode": "SUCCESS",
  "statusText": "Password Deleted Successfully",
  "data": true
}
```
### Testing
Unit tests are written for critical service methods and data operations. 

## Documentation 
**Swagger:** Access the API documentation at https://localhost:44348/swagger/index.html


## Acknowledgments
- **.NET Team**: For developing the .NET 6 framework, which powers this project.
- **Microsoft**: For providing the SQL Server and Entity Framework Core, which are integral to our data handling.
- **Swashbuckle Team**: For the Swashbuckle.AspNetCore library, which enables seamless API documentation with Swagger.
- **Docker Team**: For the Docker platform, which simplifies containerizing and managing SQL Server instances.




