# L-Connect: Logistics Management System

## Project Overview

### Vision Statement:
To streamline small-scale logistics operations by providing an accessible digital platform that bridges the gap between manual processes and enterprise-level solutions.

### Mission Statement:
To empower small logistics companies with affordable, efficient digital tools that streamline their operations, enhance client communication, and improve overall service delivery in international cargo transportation.

### Business Context:
L-Connect is a streamlined logistics management system designed to address the digital transformation needs of small logistics companies. Currently, many small logistics businesses rely on manual processes and social media to manage their operations, lacking access to sophisticated digital tools.

### Problem Statement:
Most logistics software is designed for large enterprises, leaving smaller companies underserved. L-Connect aims to fill this critical gap by offering a lightweight, affordable solution tailored to small businesses' unique workflows and client management needs.

### Target Market:
- Small logistics companies with limited digital infrastructure
- Businesses looking for efficient tracking and management solutions
- International parcel transportation service providers

## Key Features (Planned/Implemented):
- Comprehensive parcel tracking
- Client communication management
- Operational workflow digitization
- Affordable and lightweight solution
- User-friendly interface

## Project Structure
```
L-Connect/
├── Controllers/
│   ├── AccountController.cs        # Authentication & user management - created
│   ├── AdminController.cs          # Admin features - created
│   ├── ClientController.cs         # Client dashboard & features - created
│   ├── HomeController.cs           # Public pages - created
│   ├── TrackingController.cs       # Tracking functionality - created
│   └── QuoteController.cs          # Quote requests
├── Data/
│   ├── Migrations/
│   └── ApplicationDbContext.cs     # - created
├── Models/
│   ├── Domain/
│   │   ├── Role.cs                 # User roles - created
│   │   ├── User.cs                 # User information - created
│   │   ├── Shipment.cs             # Shipment tracking - created
│   │   └── ShipmentStatus.cs       # Shipment Status information  - created
│   │   ├── Quote.cs                # Quote requests
│   │   └── Document.cs             # File attachments
│   ├── ViewModels/
│   │   ├── Auth/
│   │   │   ├── LoginViewModel.cs   # Login form data - created
│   │   │   └── RegisterViewModel.cs    # Registration form data - created
│   │   ├── Tracking/
│   │   │   ├── TrackingResultViewModel.cs  # Tracking result data - created
│   │   │   └── TrackingViewModel.cs # Tracking data - created
│   │   └── Client/
│   │       └── ClientDashboardViewModel.cs # Client dashboard data - created
│   │   └── Quote/
│   │       └── QuoteViewModel.cs
│   └── ErrorViewModel.cs # Error handling - created
├── Services/
│   ├── Interfaces/
│   │   ├── IUserService.cs
│   │   ├── IShipmentService.cs # Shipment tracking service Interface - created
│   │   └── IQuoteService.cs
│   └── Implementations/
│       ├── UserService.cs
│       ├── ShipmentService.cs # Shipment tracking service Implementations - created
│       └── QuoteService.cs
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml # Login page - created
│   │   └── Register.cshtml # Registration page - created
│   ├── Admin/
│   │   ├── Dashboard.cshtml # Admin dashboard - created
│   │   ├── Shipments/
│   │   │   ├── Create.cshtml
│   │   │   └── List.cshtml
│   │   └── Reports/
│   │       └── Index.cshtml
│   ├── Client/
│   │   ├── Dashboard.cshtml # Client dashboard - created
│   │   └── Shipments/
│   │       └── List.cshtml
│   ├── Home/
│   │   ├── Index.cshtml           # Landing page - created
│   │   └── Privacy.cshtml
│   ├── Tracking/
│   │   ├── Index.cshtml           # Tracking search page - created
│   │   └── Result.cshtml          # Tracking results page - created
│   ├── Quote/
│   │   └── Request.cshtml         # Quote form
│   └── Shared/
│       ├── _AdminLayout.cshtml    # Admin layout - created
│       ├── _ClientLayout.cshtml   # Client layout - created
│       ├── _Layout.cshtml         # Main layout - created
│       ├── _ValidationScriptsPartial.cshtml # Validation scripts - created
│       └── _ViewImports.cshtml
│── wwwroot/
│   ├── css/
│   │   └── site.css
│   ├── js/
│   │   ├── tracking.js
│   │   ├── quote.js
│   │   └── site.js # Site scripts - created
│   ├── lib/                       # Third-party libraries
│   └── images/
└── Program.cs # Entry point - created
```

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (Version X.X or later)
- [Visual Studio Code](https://code.visualstudio.com/)
- [C# Extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Initial Setup

### 1. Clone the Repository
```bash
git clone https://github.com/NanmiZimikCAD/L-Connect.git
cd L-Connect
```

### 2. Install VS Code Extensions
Recommended extensions:
- C# 
- C# Dev Kit
- NuGet Package Manager
- SQL Server (optional)

### 3. Restore Dependencies
Open the terminal in VS Code and run:
```bash
dotnet restore
```

### 4. Database Configuration
- Update the connection string in `appsettings.json`
- Run database migrations:
```bash
dotnet ef database update
```

### 5. Build the Project
```bash
dotnet build
```

### 6. Run the Application
```bash
dotnet run
```

### VS Code Debugging
1. Open the Run and Debug view (Ctrl+Shift+D)
2. Select the ".NET Core Launch (web)" configuration
3. Press F5 to start debugging

## Development Workflow

### Branching Strategy
```bash
git checkout -b feature/your-feature-name
```

### Pulling Latest Changes
```bash
git pull origin main
```

## Troubleshooting
- Ensure .NET SDK is installed correctly
- Verify VS Code C# extensions are up to date
- Check connection string in `appsettings.json`

## Contributing
1. Pull latest changes
2. Create a feature branch
3. Make your changes
4. Push your branch
5. Create a pull request

## Notes
- This project uses .NET MVC with VS Code
- Refer to team lead for specific project details

## Contact
- Team Lead: [nzimik5599@conestogac.on.ca]
- Developer: [tbano9947@conestogac.on.ca]
- Developer: [srapol7701@conestogac.on.ca]
- Developer: [yhuang3398@conestogac.on.ca]
```

## Future Roadmap
- Enhance feature set
- Improve user experience
- Expand market reach# L-Connect Project
