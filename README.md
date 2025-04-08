# L-Connect: Logistics Management System

<p align="center">
  <img src="path/to/logo.png" alt="L-Connect Logo" width="200"/>
</p>

<p align="center">
  <a href="#overview">Overview</a> •
  <a href="#key-features">Key Features</a> •
  <a href="#prerequisites">Prerequisites</a> •
  <a href="#quick-setup">Quick Setup</a> •
  <a href="#documentation">Documentation</a> •
  <a href="#team">Team</a> •
  <a href="#license">License</a>
</p>

## Overview

L-Connect is a streamlined logistics management system designed specifically for small logistics companies transitioning from manual to digital operations. This intelligent platform bridges the gap between basic manual processes and complex enterprise solutions, offering essential digital tools for cargo tracking, document management, and client interactions.

### Vision Statement
To streamline small-scale logistics operations by providing an accessible digital platform that bridges the gap between manual processes and enterprise-level solutions.

### Target Market
- 🏢 Small logistics companies with limited digital infrastructure
- 🔍 Businesses looking for efficient tracking and management solutions
- 🌐 International parcel transportation service providers

## Key Features

L-Connect provides a comprehensive suite of logistics management tools:

- 🔒 **User Authentication System** - Secure role-based access control for administrators and clients
- 📦 **Comprehensive Shipment Tracking** - Real-time tracking for all shipments with detailed status history
- 📄 **Document Management** - Secure storage and retrieval of shipping documents
- 💰 **Dynamic Quote System** - Instant price quotes based on route, weight, and transportation method
- 📊 **Reporting and Analytics** - Data-driven insights into shipping operations and performance
- 👥 **Client Portal and Dashboard** - Personalized client access to shipments and documents
- 🌍 **Multi-lingual Support** - (Planned) Localization for international users
- 📱 **Mobile Application Support** - (Planned) Mobile access for on-the-go management

### Recently Added: Enhanced Quote System (March 2025)
The system now features a comprehensive shipping quote calculator that:
- Uses consistent pricing data across services and quotes
- Provides dynamic calculations based on locations, weight, and transport method
- Supports bulk pricing discounts
- Includes optional services (custom handling, insurance)
- Shows detailed cost breakdowns
- Offers real-time price estimation during form completion

## Prerequisites

Before installing L-Connect, ensure your system meets the following requirements:

- 🔷 **XAMPP 8.0+** (includes Apache, MySQL, PHP)
- 🔷 **.NET 6.0 SDK** or newer
- 🔷 **Git** for repository access
- 🔷 **Visual Studio Code** or Visual Studio 2022 (for development)

## Quick Setup

1. **Install XAMPP**
   ```
   Download and install XAMPP from apachefriends.org
   ```

2. **Clone Repository**
   ```bash
   git clone https://github.com/YourOrganization/L-Connect.git
   cd L-Connect
   ```

3. **Start MySQL in XAMPP**
   ```
   Launch XAMPP Control Panel and start MySQL and Apache services
   ```

4. **Create Database**
   ```
   Navigate to http://localhost/phpmyadmin/
   Create new database named 'l_connect' with utf8mb4_unicode_ci collation
   ```

5. **Configure Connection**
   ```bash
   # Edit appsettings.json with your database connection details
   ```

6. **Apply Migrations and Run**
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

7. **Access Application**
   ```
   Open browser and navigate to https://localhost:5001
   Login with admin@test.com / admin123
   ```

For detailed installation instructions, please refer to the [Deployment Guide](docs/DeploymentGuide.md).

## Project Structure

```
L-Connect/
├── Controllers/              # MVC Controllers by functional area
├── Data/                     # Database context and migrations
├── Models/                   # Domain and view models
│   ├── Domain/               # Database entity models
│   └── ViewModels/           # Screen-specific data models
├── Services/                 # Business logic implementation
├── Views/                    # Razor view templates
└── wwwroot/                  # Static assets (CSS, JS, images)
```

## Documentation

- [Deployment Guide](docs/DeploymentGuide.md) - Detailed installation and configuration instructions
- [User Manual](docs/UserManual.md) - Complete guide for using the application
- [Database Schema](docs/DatabaseSchema.md) - Entity relationship diagrams and database details

## Team

- **Nanmi Zimik** - Team Lead/Developer - [nzimik5599@conestogac.on.ca]
- **Tasnim Bano** - Developer - [tbano9947@conestogac.on.ca]
- **Shresta Rapol** - Developer - [srapol7701@conestogac.on.ca]
- **Yipeng Huang** - Developer - [yhuang3398@conestogac.on.ca]

## License

© 2025 L-Connect Development Team. All rights reserved.