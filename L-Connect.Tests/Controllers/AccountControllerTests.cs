using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using L_Connect.Controllers;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Tests.TestUtilities;
using Microsoft.AspNetCore.Identity;

namespace L_Connect.Tests.Controllers
{
    public class AccountControllerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly Mock<ILogger<AccountController>> _loggerMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            // Setup in-memory database context
            _context = TestDbContextFactory.CreateDbContextWithTestData();

            // Create mocks
            _userManagerMock = CreateMockUserManager();
            _signInManagerMock = CreateMockSignInManager(_userManagerMock);
            _loggerMock = new Mock<ILogger<AccountController>>();

            // Attempt to create controller
            _controller = CreateAccountController();

            // Setup controller context
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        private Mock<UserManager<User>> CreateMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                userStoreMock.Object, 
                null, 
                null, 
                null, 
                null, 
                null, 
                null, 
                null, 
                null
            );
        }

        private Mock<SignInManager<User>> CreateMockSignInManager(Mock<UserManager<User>> userManagerMock)
        {
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
            var signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                contextAccessorMock.Object,
                userPrincipalFactoryMock.Object,
                null,
                null,
                null,
                null
            );

            // Setup SignOutAsync method
            signInManagerMock
                .Setup(x => x.SignOutAsync())
                .Returns(Task.CompletedTask);

            return signInManagerMock;
        }

        private AccountController CreateAccountController()
        {
            // Get all constructors of AccountController
            var constructors = typeof(AccountController).GetConstructors();

            foreach (var constructor in constructors)
            {
                try 
                {
                    var parameters = constructor.GetParameters();
                    var args = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;
                        
                        if (paramType == typeof(ApplicationDbContext))
                            args[i] = _context;
                        else if (paramType == typeof(UserManager<User>))
                            args[i] = _userManagerMock.Object;
                        else if (paramType == typeof(SignInManager<User>))
                            args[i] = _signInManagerMock.Object;
                        else if (paramType == typeof(ILogger<AccountController>))
                            args[i] = _loggerMock.Object;
                        else
                            args[i] = null;
                    }

                    // Attempt to create controller
                    var controller = constructor.Invoke(args) as AccountController;
                    
                    if (controller != null)
                        return controller;
                }
                catch (Exception ex)
                {
                    // Log or handle specific exceptions if needed
                    Console.WriteLine($"Failed to create controller with constructor: {constructor}");
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            // If no constructor works, throw a more informative exception
            throw new InvalidOperationException(
                "Could not create AccountController. " +
                "No matching constructor found. " +
                "Please verify the AccountController constructor signature."
            );
        }

        [Fact]
        public async Task Logout_ReturnsRedirectToHomeIndex_AfterSigningOut()
        {
            // Act
            var result = await _controller.Logout() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Home", result.ControllerName);
            Assert.Equal("Index", result.ActionName);

            // Verify SignOutAsync was called
            _signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}