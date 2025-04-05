using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using L_Connect.Controllers;
using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Tracking;
using L_Connect.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace L_Connect.Tests.Controllers
{
    public class TrackingControllerTests
    {
        private readonly Mock<IShipmentService> _mockShipmentService;
        private readonly TrackingController _controller;

        public TrackingControllerTests()
        {
            _mockShipmentService = new Mock<IShipmentService>();
            
            // Updated constructor to match actual implementation (no longer has ILogger parameter)
            _controller = new TrackingController(_mockShipmentService.Object);
            
            // Setup default HttpContext
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        [Fact]
        public void Index_GET_ReturnsViewResultWithModel()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<TrackingViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Index_POST_RedirectsToResult_WhenModelStateIsValid()
        {
            // Arrange
            var model = new TrackingViewModel
            {
                TrackingNumber = "LC-123456"
            };
            
            _mockShipmentService.Setup(s => s.ValidateTrackingNumberAsync(model.TrackingNumber))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Index(model);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Result", redirectResult.ActionName);
            Assert.Equal(model.TrackingNumber, redirectResult.RouteValues["trackingNumber"]);
        }

        [Fact]
        public async Task Index_POST_ReturnsViewWithModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var model = new TrackingViewModel();
            _controller.ModelState.AddModelError("TrackingNumber", "Required");

            // Act
            var result = await _controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Same(model, viewResult.Model);
        }
        
        [Fact]
        public async Task Index_POST_ReturnsViewWithModel_WhenTrackingNumberIsInvalid()
        {
            // Arrange
            var model = new TrackingViewModel
            {
                TrackingNumber = "INVALID-NUMBER"
            };
            
            _mockShipmentService.Setup(s => s.ValidateTrackingNumberAsync(model.TrackingNumber))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Index(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Same(model, viewResult.Model);
            Assert.True(_controller.ModelState.ContainsKey("TrackingNumber"));
        }

        [Fact]
        public async Task Result_RedirectsToIndex_WhenTrackingNumberIsNull()
        {
            // Act
            var result = await _controller.Result(null);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Result_ReturnsViewWithModel_WhenTrackingNumberExists()
        {
            // Arrange
            var trackingNumber = "LC-123456";
            var shipment = new Shipment
            {
                ShipmentId = 1,
                TrackingNumber = trackingNumber,
                ClientId = 2,
                CurrentStatus = "IN_TRANSIT",
                CurrentLocation = "Transit Hub",
                OriginAddress = "Origin Address",
                DestinationAddress = "Destination Address",
                CreatedAt = DateTime.Now.AddDays(-5),
                EstimatedDeliveryDate = DateTime.Now.AddDays(2)
            };

            var statusHistory = new List<ShipmentStatus>
            {
                new ShipmentStatus
                {
                    StatusId = 1,
                    ShipmentId = 1,
                    Status = "PENDING",
                    Location = "Origin",
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new ShipmentStatus
                {
                    StatusId = 2,
                    ShipmentId = 1,
                    Status = "IN_TRANSIT",
                    Location = "Transit Hub",
                    UpdatedAt = DateTime.Now.AddDays(-3)
                }
            };

            _mockShipmentService.Setup(s => s.GetShipmentByTrackingNumberAsync(trackingNumber))
                .ReturnsAsync(shipment);
            _mockShipmentService.Setup(s => s.GetShipmentsByStatusHistoryAsync(shipment.ShipmentId))
                .ReturnsAsync(statusHistory);

            // Act
            var result = await _controller.Result(trackingNumber);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TrackingResultViewModel>(viewResult.Model);
            Assert.Equal(trackingNumber, model.TrackingNumber);
            Assert.Equal("IN_TRANSIT", model.CurrentStatus);
            Assert.Equal(2, model.StatusHistory.Count);
            Assert.Equal("Transit Hub", model.CurrentLocation);
            Assert.Equal("Origin Address", model.OriginAddress);
            Assert.Equal("Destination Address", model.DestinationAddress);
            Assert.False(model.CanAccessDocuments); // Default user is not authenticated
        }
        
        [Fact]
        public async Task Result_SetsCanAccessDocumentsToTrue_WhenUserIsClientRole()
        {
            // Arrange
            var trackingNumber = "LC-123456";
            var shipment = new Shipment
            {
                ShipmentId = 1,
                TrackingNumber = trackingNumber,
                ClientId = 2,
                CurrentStatus = "IN_TRANSIT",
                OriginAddress = "Origin Address",
                DestinationAddress = "Destination Address",
                CreatedAt = DateTime.Now.AddDays(-5)
            };

            _mockShipmentService.Setup(s => s.GetShipmentByTrackingNumberAsync(trackingNumber))
                .ReturnsAsync(shipment);
            _mockShipmentService.Setup(s => s.GetShipmentsByStatusHistoryAsync(shipment.ShipmentId))
                .ReturnsAsync(new List<ShipmentStatus>());

            // Setup authenticated user with CLIENT role
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "CLIENT"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Also need to setup User.Identity.IsAuthenticated
            var mockIdentity = new Mock<ClaimsIdentity>();
            mockIdentity.Setup(i => i.IsAuthenticated).Returns(true);
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(p => p.Identity).Returns(mockIdentity.Object);
            mockPrincipal.Setup(p => p.IsInRole("CLIENT")).Returns(true);
            
            _controller.ControllerContext.HttpContext.User = mockPrincipal.Object;

            // Act
            var result = await _controller.Result(trackingNumber);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<TrackingResultViewModel>(viewResult.Model);
            Assert.True(model.CanAccessDocuments);
        }

        [Fact]
        public async Task Result_ReturnsNotFoundView_WhenTrackingNumberDoesNotExist()
        {
            // Arrange
            var trackingNumber = "NON-EXISTENT";
            _mockShipmentService.Setup(s => s.GetShipmentByTrackingNumberAsync(trackingNumber))
                .ReturnsAsync((Shipment)null);

            // Act
            var result = await _controller.Result(trackingNumber);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NotFound", viewResult.ViewName);
            Assert.Equal(trackingNumber, viewResult.Model);
        }
    }
}