using Microsoft.EntityFrameworkCore;
using Pustok.DataAccess.Constants;

namespace Pustok.DataAccess.DataInitalizers;

internal static class SeedDataHelper
{

    public static void AddSeedData(this ModelBuilder modelBuilder)
    {

        Category defaultCategory = new()
        {
            Id = Guid.Parse("ddee9e04-e981-4bef-b9e0-bac96b98a78a"),
            //Id = Guid.NewGuid(),
            Name = "Default Category"
        };


        modelBuilder.Entity<Category>().HasData(defaultCategory);


        modelBuilder.Entity<Status>().HasData(SeedData.PendingStatus, SeedData.DoneStatus);
    }
}
