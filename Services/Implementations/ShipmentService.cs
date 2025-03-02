using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace L_Connect.Services.Implementations
{
    // Service implementation for shipment-related operations
    public class ShipmentService: IShipmentService
    {
        // Declares a private field to store a reference to the database context
        // readonly means it can only be assigned once (in the constructor)
        // This field will be used throughout the service to access the database
        // dependency injection will be used to provide the database context - The service doesn't create its own database context; it receives one from outside
        private readonly ApplicationDbContext _context;
        //constructor
        public ShipmentService(ApplicationDbContext context)
        {
            //Assigns the provided context to the private field for later use
            //stores the database context so it can be used in all the service methods
            _context = context;
        }
        // Retrieves a shipment by its tracking number
        public async Task<Shipment> GetShipmentByTrackingNumberAsync(string trackingNumber)
        {
            //performs an asynchronous database query to find a shipment
            //_context.Shipments - Accesses the Shipments DbSet in your database context
            //.FirstOrDefaultAsync(...) - Executes an asynchronous query that:
            // Returns the first matching record if found
            // Returns null if no matching record exists
            if (string.IsNullOrEmpty(trackingNumber))
                return null;

            return await _context.Shipments
            // A lambda expression that defines the search condition
            // Makes the method wait for the database query to complete before proceeding   
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
        }

        public async Task<List<ShipmentStatus>> GetShipmentsByStatusHistoryAsync(int shipmentId)
        {
            // Return all status updates for the given shipment, ordered by date (newest first)
            return await _context.ShipmentStatuses
                .Where(s => s.ShipmentId == shipmentId)
                .OrderByDescending(s => s.UpdatedAt)
                .ToListAsync();
                }

        //Retrieves all status updates for a specific shipment
        public async Task<List<ShipmentStatus>> GetShipmentStatusHistoryAsync(int shipmentId)
        {
            return await _context.ShipmentStatuses
                .Where(s => s.ShipmentId == shipmentId)
                .OrderByDescending(s => s.UpdatedAt)
                .ToListAsync();
        }
        // Validates if a tracking number exists in the system
        public async Task<bool> ValidateTrackingNumberAsync(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
                return false;

            return await _context.Shipments
                .AnyAsync(s => s.TrackingNumber == trackingNumber);
        }
        //Add the service method to fetch client shipments
        public async Task<List<Shipment>> GetShipmentsByClientIdAsync(int clientId)
        {
            return await _context.Shipments
                .Where(s => s.ClientId == clientId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }
    }
}