using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.Domain;

namespace L_Connect.Services.Interfaces
{
    // Interface for shipment-related operations, including tracking and status management
    // contains the business logic for retrieving shipment information.
    public interface IShipmentService
    {
        // Retrieves a shipment by its tracking number
        // <param name="trackingNumber">The tracking number to search for</param>
        // <returns>The shipment if found, or null if not found</returns>
        Task<Shipment> GetShipmentByTrackingNumberAsync(string trackingNumber);
        
        // Retrieves all status updates for a specific shipment
        // <param name="shipmentId">The ID of the shipment</param>
        // <returns>A list of status updates ordered by date</returns>
        Task<List<ShipmentStatus>> GetShipmentsByStatusHistoryAsync(int shipmentId);
        
        // Validates if a tracking number exists in the system
        // <param name="trackingNumber">The tracking number to validate</param>
        // <returns>True if the tracking number exists, false otherwise</returns>
        Task<bool> ValidateTrackingNumberAsync(string trackingNumber);
        
        //Add the service method to fetch client shipments
        Task<List<Shipment>> GetShipmentsByClientIdAsync(int clientId);
        
        // Retrieves all status updates for a specific shipment
        Task<List<ShipmentStatus>> GetShipmentStatusHistoryAsync(int shipmentId);
        
        // Creates a new shipment in the system with automatic tracking number generation
        // <param name="shipment">The shipment object with initial data</param>
        // <returns>The created shipment with generated tracking number and ID</returns>
        Task<Shipment> CreateShipmentAsync(Shipment shipment);
        
        // Retrieves a shipment by its ID with all related status history
        // <param name="id">The shipment ID to look up</param>
        // <returns>The shipment if found, or null if not found</returns>
        Task<Shipment> GetShipmentByIdAsync(int id);
        
        // Updates the status of an existing shipment and creates a status history record
        // <param name="shipmentId">The ID of the shipment to update</param>
        // <param name="status">The new shipment status value</param>
        // <param name="location">The current shipment location</param>
        // <param name="notes">Additional notes about the status update</param>
        // <param name="updatedByAdminId">ID of the admin making the update</param>
        // <returns>True if the update succeeded, false if the shipment wasn't found</returns>
        Task<bool> UpdateShipmentStatusAsync(int shipmentId, string status, string location, string notes, int updatedByAdminId);

        Task<List<Shipment>> GetAllShipmentsAsync();
    }
}