# Project Structure

The Ranger is an ASP.NET Web Forms application. The structure below keeps the existing Web Forms page layout intact while separating shared application code, configuration, static assets, and documentation conceptually.

```text
Ranger-Management-Sytem/
├── App_Code/                    # Shared application/business helpers
│   ├── Database.cs              # MySQL connection helpers
│   ├── DatabaseInitializer.cs   # Development seed/initialisation logic
│   ├── ImageHelper.cs           # Image selection and fallback logic
│   ├── PasswordHelper.cs        # Password hashing/verification helpers
│   └── PdfReportHelper.cs       # Booking/report generation helpers
│
├── App_Start/                   # Application startup configuration
│   ├── BundleConfig.cs
│   └── RouteConfig.cs
│
├── Content/                     # Bootstrap and site stylesheet assets
├── Images/                      # Application images used by accommodation/game-drive content
├── The ranger images/           # UI screenshots and project documentation images
├── Scripts/                     # jQuery, Bootstrap and ASP.NET Web Forms scripts
│
├── Properties/                  # .NET assembly metadata
├── docs/                        # Project documentation
│   └── PROJECT-STRUCTURE.md
│
├── *.aspx                       # Public/client and owner Web Forms pages
├── *.aspx.cs                    # Code-behind for each Web Forms page
├── *.aspx.designer.cs           # Visual Studio generated designer files
├── Site.Master                  # Shared site layout/navigation
├── Global.asax                  # Application lifecycle entry point
├── Web.config                   # Runtime and MySQL configuration
├── DatabaseUpgrade-Optional.sql # Optional migration for older databases
├── The Ranger.csproj            # Visual Studio web application project
└── The Ranger.sln               # Visual Studio solution
```

## Why the Web Forms pages remain at the project root

The application uses the classic ASP.NET Web Application/Web Forms model. Moving `.aspx`, designer, master-page, and code-behind files into new directories would require coordinated changes to project metadata, page inheritance paths, resource paths, and runtime configuration. The current structure therefore keeps executable Web Forms pages where Visual Studio expects them while grouping shared code and assets in their established folders.

## Key architectural areas

- **Presentation:** `.aspx` pages, master pages, Bootstrap and `site.css`.
- **Application logic:** code-behind files plus reusable helpers in `App_Code`.
- **Persistence:** MySQL through `MySql.Data` and the existing `SafariManagement` schema.
- **Media:** `Images/` for application imagery and `The ranger images/` for UI screenshots.
- **Documentation:** `README.md` for the project overview and `docs/` for supporting technical documentation.

This approach improves discoverability without introducing risky file moves that could break the existing Web Forms application.
