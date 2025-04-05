using System;
using Microsoft.EntityFrameworkCore;
using L_Connect.Data;

namespace L_Connect.Tests.TestUtilities
{
    public static class TestDbContextFactory
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        public static ApplicationDbContext CreateDbContextWithTestData()
        {
            var dbContext = CreateDbContext();
            
            try 
            {
                // Use the main SeedTestData method
                TestDataGenerator.SeedTestData(dbContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding test data: {ex.Message}");
                throw;
            }
            
            return dbContext;
        }
    }
}