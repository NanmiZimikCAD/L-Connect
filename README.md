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
│   ├── AccountController.cs        # Authentication & user management
│   ├── AdminController.cs          # Admin features
│   ├── ClientController.cs         # Client dashboard & features
│   ├── HomeController.cs           # Public pages
│   ├── TrackingController.cs       # Tracking functionality
│   └── QuoteController.cs          # Quote requests
├── Data/
│   ├── Migrations/
│   └── ApplicationDbContext.cs
├── Models/
│   ├── Domain/
│   │   ├── Role.cs                 # User roles
│   │   ├── User.cs                 # User information
│   │   ├── Shipment.cs             # Shipment tracking
│   │   └── ShipmentStatus.cs       # Shipment Status information
│   │   ├── Quote.cs                # Quote requests
│   │   └── Document.cs             # File attachments
│   ├── ViewModels/
│   │   ├── Auth/
│   │   │   ├── LoginViewModel.cs
│   │   │   └── RegisterViewModel.cs
│   │   ├── Tracking/
│   │   │   └── TrackingViewModel.cs
│   │   └── Client/
│   │       └── ClientDashboardViewModel.cs
│   │   └── Quote/
│   │       └── QuoteViewModel.cs
│   └── ErrorViewModel.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IUserService.cs
│   │   ├── IShipmentService.cs
│   │   └── IQuoteService.cs
│   └── Implementations/
│       ├── UserService.cs
│       ├── ShipmentService.cs
│       └── QuoteService.cs
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   ├── Admin/
│   │   ├── Dashboard.cshtml
│   │   ├── Shipments/
│   │   │   ├── Create.cshtml
│   │   │   └── List.cshtml
│   │   └── Reports/
│   │       └── Index.cshtml
│   ├── Client/
│   │   ├── Dashboard.cshtml
│   │   └── Shipments/
│   │       └── List.cshtml
│   ├── Home/
│   │   ├── Index.cshtml           # Landing page
│   │   └── Privacy.cshtml
│   ├── Tracking/
│   │   ├── Index.cshtml           # Tracking search
│   │   └── Result.cshtml          # Tracking results
│   ├── Quote/
│   │   └── Request.cshtml         # Quote form
│   └── Shared/
│       ├── _AdminLayout.cshtml    # Admin layout
│       ├── _ClientLayout.cshtml   # Client layout
│       ├── _Layout.cshtml         # Main layout
│       ├── _ValidationScriptsPartial.cshtml
│       └── _ViewImports.cshtml
└── wwwroot/
    ├── css/
    │   └── site.css
    ├── js/
    │   ├── tracking.js
    │   ├── quote.js
    │   └── site.js
    ├── lib/                       # Third-party libraries
    └── images/
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
