using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Contexts;

namespace Pustok.DataAccess.Interceptors;

internal class BaseAuditableInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditableDatas(eventData);
        return base.SavingChanges(eventData, result);
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateAuditableDatas(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableDatas(DbContextEventData eventData)
    {
        if (eventData.Context is AppDbContext appDbContext)
        {
            var entries = appDbContext.ChangeTracker.Entries<BaseAuditableEntity>().ToList();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "Admin User";
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "Admin User";
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.DeletedDate = DateTime.UtcNow;
                        entry.Entity.DeletedBy = "Admin User";
                        entry.Entity.IsDeleted = true;
                        break;


                }
            }
        }
    }


}
