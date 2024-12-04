using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasOne(e => e.Durum).WithMany().HasForeignKey(e => e.DurumId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.OlusturanPersonel).WithMany().HasForeignKey(e => e.OlusturanPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.GuncelleyenPersonel).WithMany().HasForeignKey(e => e.GuncelleyenPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.SilenPersonel).WithMany().HasForeignKey(e => e.SilenPersonelId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
