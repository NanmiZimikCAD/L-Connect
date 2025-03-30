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

## Key Features (Implemented/Planned):
- 🔒 **User Authentication System** - Complete
- 📦 **Comprehensive Shipment Tracking** - Complete
- 📄 **Document Management** - Complete
- 💰 **Dynamic Quote System** - Complete
- 📊 **Reporting and Analytics** - Complete
- 👥 **Client Portal and Dashboard** - In Progress
- 🌍 **Multi-lingual Support** - Planned
- 📱 **Mobile Application Support** - Planned

## Recently Added Features

### 💰 Enhanced Quote System (March 2025)
The system now features a comprehensive shipping quote calculator that:
- Uses consistent pricing data across services and quotes
- Provides dynamic calculations based on locations, weight, and transport method
- Supports bulk pricing discounts
- Includes optional services (custom handling, insurance)
- Shows detailed cost breakdowns
- Offers real-time price estimation during form completion

## Project Structure
```
L-Connect/
├── Controllers/
│   ├── AccountController.cs        # Authentication & user management
│   ├── AdminController.cs          # Admin features
│   ├── ClientController.cs         # Client dashboard & features
│   ├── DocumentController.cs       # Document management
│   ├── HomeController.cs           # Public pages
│   ├── QuoteRequestController.cs   # Quote calculations and requests
│   ├── ReportController.cs         # Reporting features
│   ├── ServicesController.cs       # Service pricing display
│   └── TrackingController.cs       # Tracking functionality
├── Data/
│   ├── Migrations/                 # Database migrations
│   └── ApplicationDbContext.cs     
├── Models/
│   ├── Domain/
│   │   ├── Document.cs             # File attachments
│   │   ├── Pricing.cs              # Route/service pricing
│   │   ├── Role.cs                 # User roles
│   │   ├── Shipment.cs             # Shipment tracking
│   │   ├── ShipmentStatus.cs       # Status updates 
│   │   └── User.cs                 # User information
│   ├── PricingService/             # Quote calculation models
│   │   ├── RouteInfo.cs            
│   │   └── QuoteCalculationResult.cs
│   ├── ViewModels/                 # View-specific models
│   │   ├── Auth/
│   │   ├── Client/
│   │   ├── Documents/
│   │   ├── Quote/
│   │   ├── Reports/
│   │   ├── Services/
│   │   ├── Shipments/
│   │   └── Tracking/
│   └── ErrorViewModel.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IDocumentService.cs
│   │   ├── IPricingService.cs
│   │   ├── IReportService.cs
│   │   ├── IShipmentService.cs
│   │   └── IUserService.cs
│   └── Implementations/
│       ├── DocumentService.cs
│       ├── PricingService.cs
│       ├── ReportService.cs
│       ├── ShipmentService.cs
│       └── UserService.cs
├── Views/
│   ├── Account/
│   ├── Admin/
│   ├── Client/
│   ├── Document/
│   ├── Home/
│   ├── QuoteRequest/
│   ├── Report/
│   ├── Services/
│   ├── Tracking/
│   └── Shared/
└── wwwroot/
    ├── css/
    ├── js/
    ├── lib/
    └── images/
```

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or later)
- [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio 2022
- [MySQL](https://www.mysql.com/) or [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Setup and Installation

### 1. Clone the Repository
```bash
git clone https://github.com/YourOrganization/L-Connect.git
cd L-Connect
```

### 2. Database Configuration
- Update the connection string in `appsettings.json`
- Run migrations to create the database:
```bash
dotnet ef database update
```

### 3. Run the Application
```bash
dotnet run
```

### 4. Access the Application
Open your browser and navigate to:
```
https://localhost:5001
```

## Development Notes

### Quote System
The quote system provides pricing estimates for shipping services between various locations. Pricing data is centralized in the database and used consistently throughout the application:

- `PricingService` - Handles all pricing calculations and data retrieval
- `QuoteRequestController` - Processes quote form submissions and returns estimates
- `ServicesController` - Displays service pricing options

To add new pricing:
1. Access the admin portal
2. Navigate to "Pricing Management"
3. Add new pricing entries for routes and transportation methods

## ERD Diagram
![ERD Diagram](path/to/erd-image.png)

## Contact
- Team Lead: [nzimik5599@conestogac.on.ca]
- Developer: [tbano9947@conestogac.on.ca]
- Developer: [srapol7701@conestogac.on.ca]
- Developer: [yhuang3398@conestogac.on.ca]