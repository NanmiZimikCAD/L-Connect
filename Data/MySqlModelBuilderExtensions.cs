using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace L_Connect.Data
{
    public static class MySqlModelBuilderExtensions
    {
        public static void AutoIncrementColumns(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.GetValueGenerationStrategy() == MySqlValueGenerationStrategy.IdentityColumn)
                    {
                        // Configure the property to use MySQL's auto-increment feature
                        property.SetValueGeneratorFactory((p, t) => new MySqlValueGenerator());
                    }
                }
            }
        }
    }

    public class MySqlValueGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

    protected override object NextValue(EntityEntry entry)


        {
            // Implement logic for generating the next value for auto-increment
            return null; // Placeholder, implement as needed
        }
    }
}
