using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Interceptors;

namespace Pustok.DataAccess.Contexts;

internal class AppDbContext(BaseAuditableInterceptor _interceptor, DbContextOptions options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_interceptor);

        base.OnConfiguring(optionsBuilder);
    }




    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    var entities = this.ChangeTracker.Entries<BaseAuditableEntity>().ToList();

    //    foreach (var entry in entities)
    //    {
    //        switch (entry.State)
    //        {
    //            case EntityState.Added:
    //                entry.Entity.CreatedDate = DateTime.UtcNow;
    //                entry.Entity.CreatedBy = "Admin User";
    //                break;

    //            case EntityState.Modified:
    //                entry.Entity.UpdatedDate = DateTime.UtcNow;
    //                entry.Entity.UpdatedBy = "Admin User";
    //                break;

    //            case EntityState.Deleted:
    //                entry.State= EntityState.Modified;  
    //                entry.Entity.DeletedDate = DateTime.UtcNow;
    //                entry.Entity.DeletedBy = "Admin User";
    //                entry.Entity.IsDeleted = true;
    //                break;


    //        }
    //    }

    //    return base.SaveChangesAsync(cancellationToken);
    //}
    //
    //tranfered to AuditableEntitySaveChangesInterceptor
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

}
