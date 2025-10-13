# 🏨 Booking System

[![.NET](https://img.shields.io/badge/.NET-7.0-blue)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-7.0-lightgrey)](https://docs.microsoft.com/en-us/ef/core/)

A modern **hotel booking system** built with **ASP.NET Core**, **Entity Framework Core**, and **Razor Pages**.  
Allows users to browse hotels, make bookings, and manage reservations.  

---

## 🚀 Features

- User registration and authentication with **ASP.NET Core Identity**
- Browse hotels and rooms with information
- Book rooms with date selection
- View user bookings and booking statistics
- Exception handling and input validation
- Clean architecture: Core, Persistence, Infrastructure, Application, Api layers
- Fully containerized with **Docker**

---

## 🛠 Tools & Technologies

| Layer / Tool | Description |
|--------------|-------------|
| **Backend** | ASP.NET Core Web API & Razor Pages |
| **ORM** | Entity Framework Core with MySQL |
| **Authentication** | ASP.NET Core Identity |
| **Frontend** | Razor Pages, Bootstrap CSS |
| **Database** | MySQL |
| **Containerization** | Docker |
| **Version Control** | Git & GitHub |
| **Other** | Scalar, Dapper |

---

## ⚡ Getting Started

### 1. Clone the repository

```bash
git clone [https://github.com/your-username/BookingSystemApi.git]
cd BookingSystemApi
```

### 2. Configure the database

Update the connection string in appsettings.json:

```bash
  "ConnectionStrings": {
    "BookingSystemDbContext": "Server=127.0.0.1;Port=3306;Database=BookingDb;Uid=root;Pwd=1234;SslMode=None;"
  },
```

Run migrations to create the database:

```bash
dotnet ef database update
```

### 3. Run the application

```bash
dotnet run
```

## 🎯 Usage

- Register as a new user.

- Browse hotels and rooms.

- Select dates and book a room.

- View your bookings.

- Admin can view booking statistics.


----------------------------------------------------------------------
