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
    }
}