# Ranger Management System — Project Structure

The repository uses a functional folder structure for the ASP.NET Web Forms application. Web pages are grouped by responsibility so the repository is easier to navigate and maintain.

```text
Ranger-Management-Sytem/
├── App_Code/                 Shared server-side helpers and services
├── App_Start/                Startup and routing configuration
├── Pages/
│   ├── Public/               Landing, information and catalogue pages
│   ├── Account/              Login and client registration
│   ├── Client/               Client dashboard and booking pages
│   └── Owner/                Owner dashboard and administration pages
├── Content/                  CSS and Bootstrap assets
├── Images/                   Runtime application images
├── Scripts/                  JavaScript and Web Forms assets
├── Properties/               Assembly metadata
├── docs/                     Technical documentation
├── The ranger images/        Screenshots and user-manual images
├── Site.Master               Shared application layout
├── Site.Mobile.Master        Mobile master page
├── Global.asax               Application lifecycle
├── Web.config                Runtime and database configuration
├── Bundle.config             Bundle configuration
├── The Ranger.csproj         Visual Studio project definition
└── The Ranger.sln            Visual Studio solution
```

## Page organisation

### `Pages/Public/`

Public-facing pages that do not require a client or owner dashboard:

- `Default.aspx` — landing page
- `About.aspx` — application overview
- `Contact.aspx` — support/navigation page
- `Help.aspx` — user guidance
- `Accommodation.aspx` — accommodation catalogue
- `GameDrives.aspx` — game-drive catalogue

### `Pages/Account/`

Authentication and account creation:

- `Login.aspx`
- `Register.aspx`

### `Pages/Client/`

Authenticated client functionality:

- `ClientDashboard.aspx`
- `AccommodationBooking.aspx`
- `GameDriveBooking.aspx`

### `Pages/Owner/`

Owner administration functionality:

- `OwnerDashboard.aspx`
- `OwnerAccommodation.aspx`
- `OwnerGameDrives.aspx`
- `OwnerBookings.aspx`

Each Web Forms page remains paired with its code-behind and designer files in the same directory.

## Shared code

`App_Code/` contains reusable application logic such as database access, password handling, image management, PDF reports and development database initialisation.

`App_Start/` contains application startup configuration such as bundles and routes.

## Assets

`Content/` contains CSS assets. `Scripts/` contains JavaScript libraries and ASP.NET Web Forms support scripts. `Images/` contains runtime images referenced by the application.

`The ranger images/` is intentionally separate because it contains documentation screenshots rather than runtime application assets.

## Project configuration

The `.csproj` explicitly maps the relocated Web Forms pages, code-behind files and designer files. `Web.config` sets `Pages/Public/Default.aspx` as the default document, and application links use the new `/Pages/...` paths.

This arrangement keeps the GitHub repository clean while preserving the classic ASP.NET Web Forms application model.
