# 🦁 The Ranger — Safari Management System

<p align="center">
  <img src="The%20ranger%20images/landing%20page%20.png" alt="The Ranger Safari Management System" width="900" />
</p>

<p align="center">
  <strong>A complete safari management and online booking system built with ASP.NET Web Forms, C#, and MySQL.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/C%23-.NET%20Framework%204.8.1-512BD4?logo=.net&logoColor=white" alt="C# .NET Framework 4.8.1" />
  <img src="https://img.shields.io/badge/ASP.NET-Web%20Forms-512BD4?logo=.net&logoColor=white" alt="ASP.NET Web Forms" />
  <img src="https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white" alt="MySQL 8.0" />
  <img src="https://img.shields.io/badge/Bootstrap-5.2.3-7952B3?logo=bootstrap&logoColor=white" alt="Bootstrap 5.2.3" />
  <img src="https://img.shields.io/badge/jQuery-3.7.0-0769AD?logo=jquery&logoColor=white" alt="jQuery 3.7.0" />
  <img src="https://img.shields.io/badge/License-MIT-green.svg" alt="MIT License" />
</p>

---

## 📖 Overview

**The Ranger** is a web-based safari management platform designed to make it easy for clients to discover safari accommodation and guided game drives, check availability, and manage their bookings.

The system also provides an owner-facing management area for maintaining accommodation, game drives, locations, bookings, images, and reports.

The application uses the existing **`SafariManagement` MySQL database** and is implemented using the classic **ASP.NET Web Forms** application model.

---

## ✨ Features

### 👤 Client Experience

- 🔐 Client registration and authentication
- 🏠 Browse safari accommodation
- 🔎 Filter accommodation by location and capacity
- 🦒 Browse guided game drives
- 📍 Filter game drives by location
- 📅 Select booking dates
- 👥 Capacity-aware booking validation
- ✅ Availability checking
- 🧾 View booking history
- ❌ Cancel eligible bookings
- 🖼️ Safari accommodation and wildlife imagery
- 📱 Responsive Bootstrap interface

### 🛡️ Owner Management

- 🔐 Secure owner login
- 📊 Owner dashboard
- 🏠 Create, edit and manage accommodation
- 🦒 Create, edit and manage game drives
- 📍 Manage locations
- 🖼️ Select existing images or upload new images
- 📋 Search and filter bookings
- 🔄 Update booking statuses
- 📄 Generate booking reports
- 📊 Export filtered booking information to CSV

### ⚙️ Application Improvements

- Explicit Web Forms data binding instead of fragile `Eval(...)` usage where appropriate
- Reliable database-backed image paths with fallback handling
- Stronger booking validation
- Capacity and availability checks
- Correct currency and dashboard-counter formatting
- Safer post-booking redirects
- Development seed data that avoids unnecessary duplication

---

## 🧰 Technology Stack

| Technology | Purpose |
|---|---|
| 🔷 **C#** | Application and server-side logic |
| 🌐 **ASP.NET Web Forms** | Web application framework |
| ⚙️ **.NET Framework 4.8.1** | Application runtime |
| 🐬 **MySQL 8.0** | Relational database |
| 🔌 **MySql.Data 8.0.33** | MySQL connectivity |
| 🎨 **Bootstrap 5.2.3** | Responsive UI components |
| ⚡ **jQuery 3.7.0** | Client-side scripting |
| 🧩 **Newtonsoft.Json 13.0.3** | JSON handling |
| 💻 **Visual Studio** | Recommended development environment |

---

## 🏗️ Project Structure

The repository is organised around the conventions of a classic ASP.NET Web Forms application while keeping shared code, assets and documentation separated.

```text
Ranger-Management-Sytem/
│
├── 📁 App_Code/                         # Shared application helpers
│   ├── Database.cs                      # MySQL connection helpers
│   ├── DatabaseInitializer.cs           # Development seed/initialisation
│   ├── ImageHelper.cs                   # Image selection/fallback logic
│   ├── PasswordHelper.cs                # Password hashing/verification
│   └── PdfReportHelper.cs               # Report generation
│
├── 📁 App_Start/                        # Application startup configuration
│   ├── BundleConfig.cs
│   └── RouteConfig.cs
│
├── 📁 Content/                          # CSS and Bootstrap assets
├── 📁 Images/                           # Application images
├── 📁 Scripts/                          # JavaScript and Web Forms assets
├── 📁 Properties/                       # Assembly metadata
├── 📁 docs/                             # Technical documentation
│   └── PROJECT-STRUCTURE.md
├── 📁 The ranger images/                # Screenshots and documentation images
│
├── 🌐 *.aspx                            # Web Forms pages
├── 💻 *.aspx.cs                         # Page code-behind
├── ⚙️ *.aspx.designer.cs                # Visual Studio designer files
│
├── Site.Master                          # Shared site layout/navigation
├── Site.Mobile.Master                   # Mobile master page
├── Global.asax                          # Application lifecycle
├── Web.config                           # Runtime/database configuration
├── Bundle.config                        # Bundle configuration
├── DatabaseUpgrade-Optional.sql         # Optional database migration
├── packages.config                      # NuGet dependencies
├── The Ranger.csproj                    # Visual Studio project
├── The Ranger.sln                       # Visual Studio solution
└── README.md                            # Project documentation
```

> **Note:** Web Forms pages remain at the project root because the application uses the classic ASP.NET Web Application model. Moving `.aspx`, designer and code-behind files into new folders without updating the project metadata and page/resource paths can break the application. The structure therefore prioritises a clean, conventional Web Forms layout while keeping the running application stable.

For the detailed technical map, see [`docs/PROJECT-STRUCTURE.md`](docs/PROJECT-STRUCTURE.md).

---

## 🖥️ System Screenshots

The repository includes screenshots of the implemented system.

### 🏠 Landing Page

![The Ranger landing page](The%20ranger%20images/landing%20page%20.png)

### 🔐 Login

![Client login](The%20ranger%20images/login.png)

### 📝 Registration

![Client registration](The%20ranger%20images/Register.png)

### 🏕️ Accommodation

![Accommodation catalogue](The%20ranger%20images/Acommodations.png)

### 🦒 Game Drives

![Game-drive catalogue](The%20ranger%20images/Gamedrive.png)

### 👤 Client Dashboard

![Client dashboard](The%20ranger%20images/client-Dashboard.png)

### 🏠 Accommodation Booking

![Accommodation booking](The%20ranger%20images/client-accomodation-booking.png)

### 🦒 Game-Drive Booking

![Game-drive booking](The%20ranger%20images/client-drive-booking.png)

### 🛡️ Owner Dashboard

![Owner dashboard](The%20ranger%20images/Owner-dashboard.png)

### 🏠 Manage Accommodation

![Manage accommodation](The%20ranger%20images/Manage%20accomodation.png)

### 🦒 Manage Game Drives

![Manage game drives](The%20ranger%20images/Manage%20drive.png)

### 📋 Manage Bookings

![Manage bookings](The%20ranger%20images/Manage%20bookings.png)

### 📊 Reports

![Booking report](The%20ranger%20images/Report.png)

---

## 🗄️ Database

The application uses the existing **`SafariManagement`** MySQL database.

Core database areas include:

- 👤 `Client`
- 🛡️ `Owner`
- 🏠 `Accommodation`
- 🦒 `GameDrive`
- 📋 `Bookings`
- 🚙 `DriveBookings`

The application is designed to work with the existing schema rather than destructively recreating the database during normal startup.

### 🖼️ Image Paths

Accommodation and game-drive records support database image paths. `DatabaseUpgrade-Optional.sql` can be used when an older installation is missing the required image fields or indexes.

> ⚠️ Do not run a destructive full schema reset against an existing production database. Use the optional upgrade script only when the database actually requires it.

---

## 🚀 Getting Started

### 📋 Prerequisites

Install the following before running the project:

1. 💻 Visual Studio with ASP.NET/.NET Framework development support
2. ⚙️ .NET Framework 4.8.1
3. 🐬 MySQL Server
4. 🔧 Git
5. 📦 NuGet dependencies listed in `packages.config`

### 1. 📥 Clone the repository

```bash
git clone https://github.com/Dagg12/Ranger-Management-Sytem.git
cd Ranger-Management-Sytem
```

### 2. 🐬 Configure MySQL

Make sure MySQL is running and the `SafariManagement` database exists.

Update the connection string in `Web.config` to match your local MySQL environment.

### 3. 💻 Open the solution

Open the following file in Visual Studio:

```text
The Ranger.sln
```

### 4. 📦 Restore dependencies

Restore the NuGet packages and rebuild the solution.

### 5. ▶️ Run the application

1. Select the appropriate build configuration.
2. Rebuild the solution.
3. Start the application with IIS Express.
4. Open the application in your browser.

---

## 👥 User Workflows

### 👤 Client

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
Check Availability
   ↓
Create Booking
   ↓
Client Dashboard
   ↓
View / Cancel Eligible Booking
```

### 🛡️ Owner

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

## 🔐 Booking Rules

### 🏠 Accommodation

- Only authenticated clients can create bookings.
- Check-in and check-out dates must be valid.
- Check-out must be after check-in.
- Guest counts cannot exceed accommodation capacity.
- Conflicting pending or confirmed bookings are rejected.

### 🦒 Game Drives

- Only authenticated clients can create bookings.
- Guest capacity is checked against existing bookings.
- Full drives cannot accept additional guests.

### 📋 Booking Management

- Clients can cancel eligible bookings.
- Owners can update booking status.
- Owners manage their own accommodation and game-drive inventory.
- Owner self-registration is intentionally not provided.

---

## 🌱 Development Seed Data

Development environments can initialise starter records when required records do not already exist.

A development owner account may be created when no owner exists:

```text
Email:    owner@theranger.co.za
Password: Ranger123
```

> ⚠️ Development credentials are for local/demo use only. Change or remove them before any production deployment.

---

## 🖼️ Image Management

Application imagery is stored in `Images/`, while screenshots used by the documentation are stored in `The ranger images/`.

`App_Code/ImageHelper.cs` handles image selection and fallback behaviour. Owners can select existing images or upload supported image formats when managing inventory.

---

## 🧪 Testing Checklist

- [ ] Client registration
- [ ] Client login
- [ ] Owner login
- [ ] Accommodation filtering
- [ ] Game-drive filtering
- [ ] Valid accommodation booking
- [ ] Invalid accommodation date validation
- [ ] Accommodation capacity validation
- [ ] Conflicting accommodation booking validation
- [ ] Valid game-drive booking
- [ ] Game-drive capacity validation
- [ ] Client booking history
- [ ] Client booking cancellation
- [ ] Owner accommodation CRUD operations
- [ ] Owner game-drive CRUD operations
- [ ] Owner booking search/filtering
- [ ] Booking status updates
- [ ] CSV/report generation
- [ ] Image selection and upload

---

## 🐛 Development Notes

- The application targets **.NET Framework 4.8.1** and uses classic ASP.NET Web Forms.
- MySQL must be configured before the application can operate correctly.
- The application and `SafariManagement` database schema must remain compatible.
- Development seed credentials should never be reused in production.

---

## 📚 Documentation

- 📘 [`docs/PROJECT-STRUCTURE.md`](docs/PROJECT-STRUCTURE.md) — repository and architecture guide
- 🗄️ `DatabaseUpgrade-Optional.sql` — optional database upgrade
- 📸 `The ranger images/` — system screenshots

---

## 👤 Author

**Dagg12**

🐙 GitHub: [Dagg12](https://github.com/Dagg12)

---

## 📄 License

This project will be licensed under the **MIT License** by the repository owner.

The `LICENSE` file can be added separately by the owner.

---

<p align="center">
  <strong>🦁 The Ranger</strong><br />
  <sub>Wild. Simple. Memorable.</sub>
</p>
