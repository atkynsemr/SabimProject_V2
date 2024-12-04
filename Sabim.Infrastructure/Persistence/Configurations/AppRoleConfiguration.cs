using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRole");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(r => r.Name).HasMaxLength(50).IsUnicode().IsRequired();
            builder.Property(r => r.NormalizedName).HasMaxLength(50).IsUnicode().IsRequired(false);
            builder.HasIndex(r => r.Name).IsUnique();
            builder.HasIndex(r => r.NormalizedName).IsUnique();
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(r => r.OlusturanPersonelId).HasColumnType("SMALLINT");
            builder.Property(r => r.GuncelleyenPersonelId).HasColumnType("SMALLINT");
            builder.Property(r => r.SilenPersonelId).HasColumnType("SMALLINT");
            builder.Property(r => r.OlusturulmaTarihi).HasColumnType("DATETIME");
            builder.Property(r => r.GuncellenmeTarihi).HasColumnType("DATETIME");
            builder.Property(r => r.SilinmeTarihi).HasColumnType("DATETIME");
            builder.Property(r => r.DurumId).HasColumnType("SMALLINT").IsRequired();
            // Relationships
            builder.HasOne(r => r.Durum).WithMany().HasForeignKey(r => r.DurumId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.OlusturanPersonel).WithMany().HasForeignKey(r => r.OlusturanPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.GuncelleyenPersonel).WithMany().HasForeignKey(r => r.GuncelleyenPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.SilenPersonel).WithMany().HasForeignKey(r => r.SilenPersonelId).OnDelete(DeleteBehavior.Restrict);           
        }
    }
}
