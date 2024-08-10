
#  Password Manager API

This backend API, built with C# .NET 6, provides endpoints for managing encrypted passwords. The API communicates with a SQL Server database, which is hosted in a Docker container. The passwords are stored encrypted using Base64 encoding.

## Technology
- **Language/Framework**: C# .NET 6
- **Database**: SQL Server (hosted in Docker)
- **ORM**: Entity Framework Core (Database-first approach)


## Setup

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/products/docker-desktop)

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/backend-repository.git
cd backend-repository
```

### 2. Set Up SQL Server with Docker
Docker-compose.yml file containes the SQL Server docker information.

```bash
docker-compose up
```

#### 3. Set Up Database

- **Update Connection String:** Modify appsettings.json to include your SQL Server connection 

```json
 "ConnectionStrings": {
   "ConnectionString": "Data Source=localhost,1433;Initial Catalog=PersonalPasswordManager;User ID=sa;Password=YourStrong!Passw0rd;Trust Server Certificate=True"
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

## Documentation 
**Swagger:** Access the API documentation at https://localhost:44348/swagger/index.html




