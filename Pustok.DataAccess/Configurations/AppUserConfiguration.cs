using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pustok.DataAccess.Configurations;

internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    void IEntityTypeConfiguration<AppUser>.Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.Fullname).IsRequired().HasMaxLength(256);
    }
}
