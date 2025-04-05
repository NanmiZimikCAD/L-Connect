using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using L_Connect.Controllers;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Shipments;
using L_Connect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace L_Connect.Tests.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<IShipmentService> _mockShipmentService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IDocumentService> _mockDocumentService;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _mockShipmentService = new Mock<IShipmentService>();
            _mockUserService = new Mock<IUserService>();
            _mockDocumentService = new Mock<IDocumentService>();
            _mockContext = new Mock<ApplicationDbContext>();
            
            // Update constructor to match actual AdminController
            _controller = new AdminController(
                _mockShipmentService.Object,
                _mockContext.Object,
                _mockUserService.Object,
                _mockDocumentService.Object
            );
        }

        [Fact]
        public async Task Dashboard_ReturnsViewResult_WithShipments()
        {
            // Arrange
            var shipments = new List<Shipment>
            {
                new Shipment { ShipmentId = 1, TrackingNumber = "LC-123456", CurrentStatus = "IN_TRANSIT" },
                new Shipment { ShipmentId = 2, TrackingNumber = "LC-789012", CurrentStatus = "DELIVERED" }
            };
            
            var searchResult = (shipments, shipments.Count);
            
            _mockShipmentService.Setup(s => s.SearchShipmentsAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(searchResult);
            
            // Act
            var result = await _controller.Dashboard(new ShipmentSearchViewModel());
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ShipmentSearchViewModel>(viewResult.Model);
            Assert.Equal(2, model.Shipments.Count);
        }
        
        [Fact]
        public void CreateShipment_GET_ReturnsViewResult()
        {
            // Act
            var result = _controller.CreateShipment();
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CreateShipmentViewModel>(viewResult.Model);
            Assert.NotNull(viewResult.ViewData["ServiceTypes"]);
        }
        
        [Fact]
        public async Task CreateShipment_POST_RedirectsToShipmentDetails_WhenModelStateIsValid()
        {
            // Arrange
            var viewModel = new CreateShipmentViewModel
            {
                ClientEmail = "client@example.com",
                Weight = 15.0m,
                OriginAddress = "Origin Test Address",
                DestinationAddress = "Destination Test Address",
                ServiceType = "Standard"
            };
            
            var user = new User { UserId = 2, Email = "client@example.com" };
            var createdShipment = new Shipment 
            { 
                ShipmentId = 1, 
                TrackingNumber = "LC-123456", 
                ClientId = 2 
            };
            
            _mockUserService.Setup(s => s.DoesUserExistByEmail(It.IsAny<string>()))
                .ReturnsAsync(true);
                
            _mockUserService.Setup(s => s.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(user);
                
            _mockShipmentService.Setup(s => s.CreateShipmentAsync(It.IsAny<Shipment>()))
                .ReturnsAsync(createdShipment);
            
            // Act
            var result = await _controller.CreateShipment(viewModel);
            
            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ShipmentDetails", redirectResult.ActionName);
            Assert.Equal(1, redirectResult.RouteValues["id"]);
            Assert.NotNull(_controller.TempData["SuccessMessage"]);
        }
        
        [Fact]
        public async Task CreateShipment_POST_ReturnsViewWithModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var viewModel = new CreateShipmentViewModel();
            _controller.ModelState.AddModelError("OriginAddress", "Required");
            
            // Act
            var result = await _controller.CreateShipment(viewModel);
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Same(viewModel, viewResult.Model);
            Assert.NotNull(viewResult.ViewData["ServiceTypes"]);
        }
        
        [Fact]
        public async Task ShipmentDetails_ReturnsViewResult_WhenShipmentExists()
        {
            // Arrange
            var shipmentId = 1;
            var shipment = new Shipment
            {
                ShipmentId = shipmentId,
                TrackingNumber = "LC-123456",
                CurrentStatus = "IN_TRANSIT"
            };
            
            var statusHistory = new List<ShipmentStatus>
            {
                new ShipmentStatus 
                { 
                    StatusId = 1, 
                    ShipmentId = shipmentId, 
                    Status = "PENDING", 
                    UpdatedAt = DateTime.Now.AddDays(-2) 
                },
                new ShipmentStatus 
                { 
                    StatusId = 2, 
                    ShipmentId = shipmentId, 
                    Status = "IN_TRANSIT", 
                    UpdatedAt = DateTime.Now.AddDays(-1) 
                }
            };
            
            _mockShipmentService.Setup(s => s.GetShipmentByIdAsync(shipmentId))
                .ReturnsAsync(shipment);
                
            _mockShipmentService.Setup(s => s.GetShipmentStatusHistoryAsync(shipmentId))
                .ReturnsAsync(statusHistory);
            
            // Act
            var result = await _controller.ShipmentDetails(shipmentId);
            
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsType<ShipmentDetailsViewModel>(viewResult.Model);
            Assert.Equal(shipment, viewModel.Shipment);
            Assert.Equal(statusHistory, viewModel.StatusHistory);
        }
        
        [Fact]
        public async Task ShipmentDetails_ReturnsNotFound_WhenShipmentDoesNotExist()
        {
            // Arrange
            var shipmentId = 999;
            
            _mockShipmentService.Setup(s => s.GetShipmentByIdAsync(shipmentId))
                .ReturnsAsync((Shipment)null);
            
            // Act
            var result = await _controller.ShipmentDetails(shipmentId);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        // Test for the CalculateShippingCost private method using reflection
        [Fact]
        public void CalculateShippingCost_CalculatesCorrectly_ForDifferentServiceTypes()
        {
            // Arrange
            var method = typeof(AdminController).GetMethod("CalculateShippingCost", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // Act & Assert
            
            // Standard service
            var standardCost = (decimal)method.Invoke(_controller, new object[] { 10.0m, "Standard" });
            Assert.Equal(35.0m, standardCost); // 10.0 base + (10.0 * 2.5 per kg)
            
            // Express service
            var expressCost = (decimal)method.Invoke(_controller, new object[] { 10.0m, "Express" });
            Assert.Equal(57.5m, expressCost); // 10.0 base + (10.0 * 4.75 per kg)
            
            // International service
            var internationalCost = (decimal)method.Invoke(_controller, new object[] { 10.0m, "International" });
            Assert.Equal(110.0m, internationalCost); // 25.0 base + (10.0 * 8.5 per kg)
        }
    }
}