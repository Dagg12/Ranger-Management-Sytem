# 🦁 The Ranger — Safari Management System

A complete safari management and booking web application built with **ASP.NET Web Forms, C#, and MySQL**. The system supports clients who want to discover accommodation and game drives, make bookings, manage their reservations, and owners who need to manage their safari inventory and bookings.

> **Academic project:** Developed for CMPG213/CMPG223 using the supplied `SafariManagement` MySQL database.

![The Ranger landing page](The%20ranger%20images/landing%20page%20.png)

## 📌 Overview

The Ranger provides two main user experiences:

- **Clients** can browse accommodation and game drives, register and sign in, make bookings, view their reservations, and cancel eligible bookings.
- **Owners** can manage their accommodation, manage game drives, review and update bookings, filter booking information, and generate booking reports.

The application is built around the existing `SafariManagement` database and is designed to work with the current database structure rather than recreating the schema on every run.

---

## ✨ Features

### Client features

- Client registration and authentication
- Client dashboard
- Accommodation browsing and filtering
- Game-drive browsing and filtering
- Accommodation booking
- Game-drive booking
- Booking validation and availability checks
- Booking history and cancellation
- Capacity-aware booking rules
- Safari and accommodation imagery
- Responsive Bootstrap-based interface

### Owner features

- Secure owner login
- Owner dashboard
- Accommodation management
- Game-drive management
- Image selection and image uploads
- Location management
- Booking management
- Booking search and filtering
- Booking status updates
- CSV booking reports
- PDF/report support

### Application reliability improvements

- Fixed ASP.NET Web Forms `Eval(...)` data-binding issues by using explicit public properties and data-bound controls.
- Improved Repeater rendering across client and owner pages.
- Added reliable image selection with database `image_path` support and fallback matching.
- Added development seed data without unnecessarily duplicating existing inventory.
- Added stronger booking validation for dates, capacity, authentication, and availability.
- Improved money and dashboard-counter formatting so values are displayed correctly.
- Added request completion/redirect handling after successful bookings.

---

## 🛠️ Technology Stack

| Technology | Purpose |
| --- | --- |
| **C#** | Application and server-side logic |
| **ASP.NET Web Forms** | Web application framework |
| **.NET Framework 4.8.1** | Application runtime/target framework |
| **MySQL** | Relational database |
| **MySql.Data 8.0.33** | MySQL connectivity |
| **Bootstrap 5.2.3** | Responsive UI and components |
| **jQuery 3.7.0** | Client-side scripting |
| **Newtonsoft.Json 13.0.3** | JSON handling |
| **Visual Studio** | Recommended development environment |

---

## 🏗️ Application Architecture

The application follows a conventional ASP.NET Web Forms structure:

```text
Browser
   │
   ▼
ASP.NET Web Forms Pages (.aspx)
   │
   ├── Client pages
   │   ├── Login / Registration
   │   ├── Accommodation
   │   ├── Game Drives
   │   ├── Booking pages
   │   └── Client Dashboard
   │
   └── Owner pages
       ├── Owner Dashboard
       ├── Accommodation Management
       ├── Game Drive Management
       └── Booking Management
   │
   ▼
Code-behind (.aspx.cs)
   │
   ▼
Shared helpers / application services
   │
   ├── Database.cs
   ├── DatabaseInitializer.cs
   ├── ImageHelper.cs
   ├── PasswordHelper.cs
   └── PdfReportHelper.cs
   │
   ▼
MySQL — SafariManagement
```

For a more detailed repository map, see [`docs/PROJECT-STRUCTURE.md`](docs/PROJECT-STRUCTURE.md).

---

## 📂 Repository Structure

```text
Ranger-Management-Sytem/
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
├── Content/                     # Bootstrap and application CSS
├── Images/                      # Application imagery
├── Scripts/                     # JavaScript and Web Forms assets
├── Properties/                  # Assembly metadata
├── The ranger images/           # UI screenshots and project images
├── docs/                        # Technical documentation
│   └── PROJECT-STRUCTURE.md
│
├── Default.aspx                 # Landing page
├── Login.aspx                   # Client/owner login
├── Register.aspx                # Client registration
├── Accommodation.aspx          # Accommodation catalogue
├── AccommodationBooking.aspx   # Accommodation booking
├── GameDrives.aspx              # Game-drive catalogue
├── GameDriveBooking.aspx        # Game-drive booking
├── ClientDashboard.aspx         # Client reservations
├── OwnerDashboard.aspx          # Owner dashboard
├── OwnerAccommodation.aspx      # Owner accommodation management
├── OwnerGameDrives.aspx         # Owner game-drive management
├── OwnerBookings.aspx            # Owner booking management
│
├── DatabaseUpgrade-Optional.sql
├── The Ranger.csproj
├── The Ranger.sln
├── Web.config
└── README.md
```

---

# 📸 System Screenshots

The repository already contains screenshots of the implemented system. The most important screens are included below so the README gives a visual overview of the application.

## Landing Page

![The Ranger landing page](The%20ranger%20images/landing%20page%20.png)

## Client Login

![Client login](The%20ranger%20images/login.png)

## Client Registration

![Client registration](The%20ranger%20images/Register.png)

## Accommodation

![Accommodation catalogue](The%20ranger%20images/Acommodations.png)

## Game Drives

![Game-drive catalogue](The%20ranger%20images/Gamedrive.png)

## Client Dashboard

![Client dashboard](The%20ranger%20images/client-Dashboard.png)

## Accommodation Booking

![Accommodation booking](The%20ranger%20images/client-accomodation-booking.png)

## Game-Drive Booking

![Game-drive booking](The%20ranger%20images/client-drive-booking.png)

## Owner Dashboard

![Owner dashboard](The%20ranger%20images/Owner-dashboard.png)

## Manage Accommodation

![Manage accommodation](The%20ranger%20images/Manage%20accomodation.png)

## Manage Game Drives

![Manage game drives](The%20ranger%20images/Manage%20drive.png)

## Manage Bookings

![Manage bookings](The%20ranger%20images/Manage%20bookings.png)

## Reports

![Booking report](The%20ranger%20images/Report.png)

---

# 🗄️ Database

The application uses the existing **`SafariManagement`** MySQL database.

The application works with these core tables:

- `Client`
- `Owner`
- `Accommodation`
- `GameDrive`
- `Bookings`
- `DriveBookings`

The application does **not** recreate or destructively reset the database on startup.

### Image fields

The current application supports image paths for accommodation and game-drive records. `DatabaseUpgrade-Optional.sql` is provided for older databases that may not yet contain the required image-path fields or indexes.

> ⚠️ Do not execute the original full database schema against a live database if it contains destructive `DROP`/recreate operations. Use the optional upgrade script only when the existing database actually requires it.

---

# 🚀 Getting Started

## Prerequisites

Before running the project, install:

1. **Visual Studio** with ASP.NET/.NET Framework development support.
2. **.NET Framework 4.8.1**.
3. **MySQL Server**.
4. **Git**.
5. The required NuGet packages listed in `packages.config`.

## 1. Clone the repository

```bash
git clone https://github.com/Dagg12/Ranger-Management-Sytem.git
cd Ranger-Management-Sytem
```

## 2. Configure MySQL

Make sure MySQL is running and that the `SafariManagement` database exists.

Review `Web.config` and update the connection string for your local MySQL environment.

For development, the project expects the database to be available before the application starts.

## 3. Open the solution

Open:

```text
The Ranger.sln
```

in Visual Studio.

## 4. Restore NuGet packages

Restore the packages configured in `packages.config`, then rebuild the solution.

## 5. Build and run

In Visual Studio:

1. Select the appropriate build configuration.
2. Rebuild the solution.
3. Start the application with IIS Express.
4. Open the site in your browser.

---

# 👥 User Workflows

## Client workflow

```text
Register
   ↓
Login
   ↓
Browse Accommodation / Game Drives
   ↓
Select Experience
   ↓
Choose Dates / Guests
   ↓
Validate Availability
   ↓
Create Booking
   ↓
Client Dashboard
   ↓
View or Cancel Eligible Booking
```

## Owner workflow

```text
Owner Login
   ↓
Owner Dashboard
   ├── Manage Accommodation
   ├── Manage Game Drives
   └── Manage Bookings
          ↓
      Search / Filter
          ↓
      Update Booking Status
          ↓
      Generate Report
```

---

# 🔐 Booking Rules

The system implements validation rules intended to prevent invalid or conflicting bookings.

### Accommodation

- Only authenticated clients can create bookings.
- Check-in and check-out dates must be valid.
- Check-out must occur after check-in.
- Guest counts cannot exceed accommodation capacity.
- Occupied dates are rejected when existing pending or confirmed bookings conflict.

### Game Drives

- Only authenticated clients can create bookings.
- Guest capacity is checked against existing pending and confirmed bookings.
- Full drives cannot accept additional guests.

### Booking management

- Clients can cancel eligible pending/confirmed bookings.
- Owners can update booking status.
- Owners manage only their own accommodation and game-drive inventory.
- Owner self-registration is intentionally not provided.

---

# 🌱 Development Seed Data

For development environments, the application can initialise starter data when the relevant records are missing.

A development owner account is created when no owner exists:

```text
Email:    owner@theranger.co.za
Password: Ranger123
```

Starter accommodation and game-drive records can also be inserted when the corresponding inventory is empty. Existing records are not intentionally duplicated when matching records already exist.

> ⚠️ These credentials are for development/demo use only. Change or remove them before deploying the application to a production environment.

---

# 🖼️ Image Management

The application separates database data from image selection through `App_Code/ImageHelper.cs`.

Images are stored in the repository under `Images/`, while the UI screenshots used for documentation are stored under `The ranger images/`.

Accommodation and game-drive records can use an image path when available, with fallback matching for older records.

Owners can select an existing image or upload supported image formats when adding or editing inventory.

---

# 🧪 Testing Checklist

Before considering a local build ready, test the following flows:

- [ ] Client registration
- [ ] Client login
- [ ] Owner login
- [ ] Accommodation search/filtering
- [ ] Game-drive search/filtering
- [ ] Valid accommodation booking
- [ ] Invalid accommodation date validation
- [ ] Accommodation capacity validation
- [ ] Conflicting accommodation booking validation
- [ ] Valid game-drive booking
- [ ] Game-drive capacity validation
- [ ] Client booking history
- [ ] Client booking cancellation
- [ ] Owner accommodation creation/edit/delete
- [ ] Owner game-drive creation/edit/delete
- [ ] Owner booking search/filtering
- [ ] Owner booking status updates
- [ ] CSV/report generation
- [ ] Image selection and upload

---

# 🐛 Known Development Considerations

- The project is built on the classic ASP.NET Web Forms model and targets .NET Framework 4.8.1.
- Local MySQL configuration is required before the application can function correctly.
- The supplied database schema and the application must remain compatible, especially around booking relationships and image-path fields.
- Development seed credentials should never be reused for a production deployment.

---

# 📚 Documentation

- [`docs/PROJECT-STRUCTURE.md`](docs/PROJECT-STRUCTURE.md) — repository structure and architectural guidance.
- `DatabaseUpgrade-Optional.sql` — optional database upgrade for older installations.
- `The ranger images/` — system screenshots and UI documentation images.

---

# 🎓 Academic Context

The Ranger was developed as a university project for **CMPG213/CMPG223**, combining database design, web application development, server-side programming, user authentication, booking logic, and user-interface design.

The project demonstrates how a traditional ASP.NET Web Forms application can be structured around a relational MySQL database while providing separate client and owner workflows.

---

# 👤 Author

**Dagg12**

GitHub: [Dagg12](https://github.com/Dagg12)

---

# 📄 License

No explicit open-source license is currently included in the repository. Unless a license is added, the project should be treated as **all rights reserved** by the repository owner.

---

## ⭐ Project

If you are reviewing or evaluating this project, the repository contains the complete ASP.NET Web Forms source, application assets, database integration code, and screenshots of the implemented system.
