using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace L_Connect.Services.Implementations
{
    // Service implementation for shipment-related operations
    public class ShipmentService: IShipmentService
    {
        private readonly ApplicationDbContext _context;
        
        public ShipmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Existing methods remain unchanged
        public async Task<Shipment> GetShipmentByTrackingNumberAsync(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
                return null;

            return await _context.Shipments
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
        }

        public async Task<List<ShipmentStatus>> GetShipmentsByStatusHistoryAsync(int shipmentId)
        {
            return await _context.ShipmentStatuses
                .Where(s => s.ShipmentId == shipmentId)
                .OrderByDescending(s => s.UpdatedAt)
                .ToListAsync();
        }

        public async Task<List<ShipmentStatus>> GetShipmentStatusHistoryAsync(int shipmentId)
        {
            return await _context.ShipmentStatuses
                .Where(s => s.ShipmentId == shipmentId)
                .OrderByDescending(s => s.UpdatedAt)
                .ToListAsync();
        }
        
        public async Task<bool> ValidateTrackingNumberAsync(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
                return false;

            return await _context.Shipments
                .AnyAsync(s => s.TrackingNumber == trackingNumber);
        }
        
        public async Task<List<Shipment>> GetShipmentsByClientIdAsync(int clientId)
        {
            return await _context.Shipments
                .Where(s => s.ClientId == clientId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        // UPDATED METHODS BELOW - Fixed to work with your Shipment model

        public async Task<Shipment> CreateShipmentAsync(Shipment shipment)
        {
            // Generate a unique tracking number
            shipment.TrackingNumber = GenerateTrackingNumber();
            
            // Set creation date
            shipment.CreatedAt = DateTime.UtcNow;
            
            // Set initial status to "Pending"
            shipment.CurrentStatus = "Pending";
            shipment.CurrentLocation = shipment.OriginAddress;
            
            // ServiceType is now directly set from the input parameter
            // No need to calculate or derive it
            
            // Calculate estimated delivery date based on service type
            int deliveryDays = 5; // Default
            
            if (shipment.ServiceType == "Express")
            {
                deliveryDays = 3;
            }
            else if (shipment.ServiceType == "International")
            {
                deliveryDays = 10;
            }
            
            shipment.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(deliveryDays);

            // Add to database
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            // Create initial status record
            var initialStatus = new ShipmentStatus
            {
                ShipmentId = shipment.ShipmentId,
                Status = "Pending",
                Location = shipment.OriginAddress,
                Notes = "Shipment created and awaiting processing",
                UpdatedAt = DateTime.UtcNow,
                UpdatedByAdminId = shipment.CreatedByAdminId
            };

            _context.ShipmentStatuses.Add(initialStatus);
            await _context.SaveChangesAsync();

            return shipment;
        }

        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            // Get shipment by ID
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.ShipmentId == id); // Note: using ShipmentId

            return shipment;
        }

        public async Task<bool> UpdateShipmentStatusAsync(int shipmentId, string status, string location, string notes, int updatedByAdminId)
        {
            // Find the shipment
            var shipment = await _context.Shipments.FindAsync(shipmentId);
            if (shipment == null)
            {
                return false;
            }

            // Update the shipment
            shipment.CurrentStatus = status;
            shipment.CurrentLocation = location;

            // Create status history record
            var statusUpdate = new ShipmentStatus
            {
                ShipmentId = shipmentId,
                Status = status,
                Location = location,
                Notes = notes,
                UpdatedAt = DateTime.UtcNow,
                UpdatedByAdminId = updatedByAdminId
            };

            _context.ShipmentStatuses.Add(statusUpdate);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GenerateTrackingNumber()
        {
            // Format: LC-YYYYMMDD-XXXX where XXXX is a random 4-digit number
            var dateComponent = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random();
            var randomComponent = random.Next(1000, 9999).ToString();
            
            return $"LC-{dateComponent}-{randomComponent}";
        }
    }
}