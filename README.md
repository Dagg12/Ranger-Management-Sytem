# The Ranger — Safari Management System

<p align="center">
  <img src="The%20ranger%20images/landing%20page%20.png" alt="The Ranger Safari Management System" width="900" />
</p>

<p align="center">
  <strong>A professional safari management and online booking platform built with ASP.NET Web Forms, C#, and MySQL.</strong>
</p>

<p align="center">
  <a href="https://github.com/Dagg12/Ranger-Management-Sytem"><img src="https://img.shields.io/badge/ASP.NET-Web%20Forms-512BD4?logo=dotnet&logoColor=white" alt="ASP.NET Web Forms" /></a>
  <img src="https://img.shields.io/badge/C%23-.NET%20Framework%204.8.1-512BD4?logo=dotnet&logoColor=white" alt="C# and .NET Framework 4.8.1" />
  <img src="https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white" alt="MySQL 8.0" />
  <img src="https://img.shields.io/badge/Bootstrap-5.2.3-7952B3?logo=bootstrap&logoColor=white" alt="Bootstrap 5.2.3" />
  <img src="https://img.shields.io/badge/jQuery-3.7.0-0769AD?logo=jquery&logoColor=white" alt="jQuery 3.7.0" />
  <img src="https://img.shields.io/badge/Font%20Awesome-6.5.2-528DD7?logo=fontawesome&logoColor=white" alt="Font Awesome 6.5.2" />
</p>

---

## Overview

**The Ranger** is a web-based safari management system that connects guests with accommodation and guided game-drive experiences through a single booking platform.

The system provides two main experiences:

- **Client portal** — registration, authentication, accommodation discovery, game-drive discovery, availability-aware bookings and booking management.
- **Owner portal** — inventory management, image management, booking administration, status updates and filtered reporting.

The application is implemented as a classic **ASP.NET Web Forms Web Application** targeting **.NET Framework 4.8.1** and using **MySQL** for persistent data.

---

## Features

### Client Portal

- Client registration and authentication
- Browse accommodation listings
- Search and filter accommodation
- Browse guided game drives
- Search and filter game drives
- Capacity-aware availability checks
- Date validation for accommodation bookings
- Guest validation for game-drive bookings
- Booking history and status tracking
- Eligible booking cancellation
- Responsive interface for desktop and mobile layouts

### Owner Portal

- Owner authentication
- Owner dashboard and operational statistics
- Accommodation CRUD management
- Game-drive CRUD management
- Availability controls
- Location management
- Existing image selection
- New image upload support
- Booking search and filtering
- Booking status management
- Filtered report preview
- PDF report generation

### Application Services

- MySQL database access
- Development database initialisation
- Password hashing and verification
- Image selection and fallback handling
- PDF report generation
- Bootstrap responsive layout
- Font Awesome interface icons
- jQuery client-side functionality

---

## Technology Stack

<p align="center">
  <img src="https://skillicons.dev/icons?i=cs,dotnet,mysql,bootstrap,jquery,git,github,visualstudio" alt="Technology stack icons" />
</p>

| Technology | Version / Role |
|---|---|
| <img src="https://cdn.simpleicons.org/dotnet/512BD4" width="22" alt=".NET" /> **C# / .NET Framework** | 4.8.1 |
| <img src="https://cdn.simpleicons.org/dotnet/512BD4" width="22" alt="ASP.NET" /> **ASP.NET Web Forms** | Web application framework |
| <img src="https://cdn.simpleicons.org/mysql/4479A1" width="22" alt="MySQL" /> **MySQL** | 8.0 database |
| <img src="https://cdn.simpleicons.org/bootstrap/7952B3" width="22" alt="Bootstrap" /> **Bootstrap** | 5.2.3 UI framework |
| <img src="https://cdn.simpleicons.org/jquery/0769AD" width="22" alt="jQuery" /> **jQuery** | 3.7.0 client-side library |
| <img src="https://cdn.simpleicons.org/fontawesome/528DD7" width="22" alt="Font Awesome" /> **Font Awesome** | 6.5.2 interface icons |
| **MySql.Data** | 8.0.33 database provider |
| **Newtonsoft.Json** | 13.0.3 JSON handling |
| <img src="https://cdn.simpleicons.org/visualstudio/5C2D91" width="22" alt="Visual Studio" /> **Visual Studio** | Recommended IDE |

---

## Project Structure

The repository is organised by application responsibility. Web Forms pages are grouped by user-facing area instead of being exposed as a large collection of files at the repository root.

```text
Ranger-Management-Sytem/
│
├── App_Code/
│   ├── Database.cs
│   ├── DatabaseInitializer.cs
│   ├── ImageHelper.cs
│   ├── PasswordHelper.cs
│   └── PdfReportHelper.cs
│
├── App_Start/
│   ├── BundleConfig.cs
│   └── RouteConfig.cs
│
├── Pages/
│   ├── Public/
│   │   ├── Default.aspx
│   │   ├── About.aspx
│   │   ├── Contact.aspx
│   │   ├── Help.aspx
│   │   ├── Accommodation.aspx
│   │   └── GameDrives.aspx
│   │
│   ├── Account/
│   │   ├── Login.aspx
│   │   └── Register.aspx
│   │
│   ├── Client/
│   │   ├── ClientDashboard.aspx
│   │   ├── AccommodationBooking.aspx
│   │   └── GameDriveBooking.aspx
│   │
│   └── Owner/
│       ├── OwnerDashboard.aspx
│       ├── OwnerAccommodation.aspx
│       ├── OwnerGameDrives.aspx
│       └── OwnerBookings.aspx
│
├── Content/
│   └── Bootstrap and application CSS
│
├── Images/
│   └── Application imagery
│
├── Scripts/
│   ├── Bootstrap and jQuery assets
│   └── WebForms support scripts
│
├── Properties/
│   └── AssemblyInfo.cs
│
├── docs/
│   └── PROJECT-STRUCTURE.md
│
├── The ranger images/
│   └── System screenshots and user-manual images
│
├── Site.Master
├── Site.Mobile.Master
├── Global.asax
├── Web.config
├── Bundle.config
├── DatabaseUpgrade-Optional.sql
├── packages.config
├── The Ranger.csproj
├── The Ranger.sln
└── README.md
```

### Why the pages are grouped this way

The application is still a standard ASP.NET Web Forms application. Each page keeps its `.aspx`, `.aspx.cs`, and `.aspx.designer.cs` files together inside the appropriate functional directory. The Visual Studio project file explicitly maps those files, while shared application infrastructure remains in `App_Code` and `App_Start`.

The application root is configured to open `Pages/Public/Default.aspx` as the default document.

---

## System Screenshots

The repository contains screenshots of the implemented application in `The ranger images/`.

### Landing Page

![The Ranger landing page](The%20ranger%20images/landing%20page%20.png)

### Login

![Client login](The%20ranger%20images/login.png)

### Registration

![Client registration](The%20ranger%20images/Register.png)

### Accommodation

![Accommodation catalogue](The%20ranger%20images/Acommodations.png)

### Game Drives

![Game-drive catalogue](The%20ranger%20images/Gamedrive.png)

### Client Dashboard

![Client dashboard](The%20ranger%20images/client-Dashboard.png)

### Accommodation Booking

![Accommodation booking](The%20ranger%20images/client-accomodation-booking.png)

### Game-Drive Booking

![Game-drive booking](The%20ranger%20images/client-drive-booking.png)

### Owner Dashboard

![Owner dashboard](The%20ranger%20images/Owner-dashboard.png)

### Manage Accommodation

![Manage accommodation](The%20ranger%20images/Manage%20accomodation.png)

### Manage Game Drives

![Manage game drives](The%20ranger%20images/Manage%20drive.png)

### Manage Bookings

![Manage bookings](The%20ranger%20images/Manage%20bookings.png)

### Reports

![Booking report](The%20ranger%20images/Report.png)

---

## Database

The application uses the **SafariManagement** MySQL database.

The main data areas are:

- `Client`
- `Owner`
- `Accommodation`
- `GameDrive`
- `Bookings`
- `DriveBookings`

`DatabaseUpgrade-Optional.sql` provides an optional upgrade path for installations that require additional image fields or indexes.

Do not run destructive schema resets against an existing production database.

---

## Getting Started

### Prerequisites

- Visual Studio with ASP.NET and .NET Framework development tools
- .NET Framework 4.8.1
- MySQL Server 8.0 or compatible version
- Git
- NuGet package restore support

### Clone the repository

```bash
git clone https://github.com/Dagg12/Ranger-Management-Sytem.git
cd Ranger-Management-Sytem
```

### Configure MySQL

Create or connect the application to the `SafariManagement` database and update the connection string in `Web.config` for your local environment.

### Open the solution

Open:

```text
The Ranger.sln
```

Restore NuGet packages, rebuild the solution and start the application using IIS Express.

The default application document points to:

```text
Pages/Public/Default.aspx
```

---

## User Workflows

### Client

```text
Register
   |
   v
Login
   |
   v
Browse Accommodation / Game Drives
   |
   v
Select Experience
   |
   v
Choose Dates / Guests
   |
   v
Check Availability
   |
   v
Create Booking
   |
   v
Client Dashboard
   |
   v
View or Cancel Eligible Booking
```

### Owner

```text
Owner Login
   |
   v
Owner Dashboard
   |
   +----------------------+----------------------+
   |                      |                      |
   v                      v                      v
Accommodation        Game Drives             Bookings
Management           Management              & Reports
   |                      |                      |
   +----------------------+----------------------+
                          |
                          v
                   Search / Filter
                          |
                          v
                  Update Status
                          |
                          v
                    Generate Report
```

---

## Booking Rules

### Accommodation

- Only authenticated clients can create bookings.
- Check-out must be later than check-in.
- Guest count cannot exceed the accommodation capacity.
- Conflicting pending or confirmed reservations are rejected.

### Game Drives

- Only authenticated clients can create bookings.
- Guest capacity is checked against existing reservations.
- Full drives cannot accept additional guests.

### Booking Management

- Clients can cancel eligible bookings.
- Owners can update booking statuses.
- Owners manage accommodation and game-drive inventory.
- Owner self-registration is not exposed through the public client registration flow.

---

## Image Management

Application images are stored in `Images/`.

Documentation screenshots are stored separately in `The ranger images/`.

`App_Code/ImageHelper.cs` provides image selection, validation and fallback handling. Owner inventory pages allow an existing application image to be selected or a supported image to be uploaded.

Supported upload formats include:

```text
.jpg
.jpeg
.png
.webp
.avif
```

---

## Development Seed Data

Development environments can initialise required starter data when records do not already exist.

If the development database creates a default owner account, change the credentials before using the application outside a local/demo environment.

---

## Testing Checklist

- [ ] Client registration
- [ ] Client login
- [ ] Owner login
- [ ] Accommodation search and filtering
- [ ] Game-drive search and filtering
- [ ] Accommodation availability validation
- [ ] Accommodation capacity validation
- [ ] Game-drive capacity validation
- [ ] Accommodation booking
- [ ] Game-drive booking
- [ ] Client booking history
- [ ] Client booking cancellation
- [ ] Owner accommodation CRUD
- [ ] Owner game-drive CRUD
- [ ] Owner booking filtering
- [ ] Booking status updates
- [ ] Report preview
- [ ] PDF report generation
- [ ] Image selection
- [ ] Image upload

---

## Documentation

- [`docs/PROJECT-STRUCTURE.md`](docs/PROJECT-STRUCTURE.md) — repository architecture and file responsibilities
- `DatabaseUpgrade-Optional.sql` — optional database upgrade script
- `The ranger images/` — application screenshots and user-manual material

---

## Author

**Dagg12**

<a href="https://github.com/Dagg12"><img src="https://img.shields.io/badge/GitHub-Dagg12-181717?logo=github&logoColor=white" alt="Dagg12 on GitHub" /></a>

---

## License

This project is intended to be licensed under the **MIT License**. The repository owner will add the `LICENSE` file separately.

---

<p align="center">
  <strong>The Ranger</strong><br />
  <sub>Wild. Simple. Memorable.</sub>
</p>
