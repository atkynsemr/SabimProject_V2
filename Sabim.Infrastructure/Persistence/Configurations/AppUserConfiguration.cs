using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).HasMaxLength(20).IsRequired();
            builder.Property(u => u.ProfilResmiYolu).HasMaxLength(255).IsUnicode(false).IsRequired(false); 
            builder.Property(u => u.SonOturumAcmaZamani).IsRequired(false);
            builder.Property(u => u.PersonelId).IsRequired();
            builder.Property(u => u.OlusturanPersonelId).IsRequired(false);
            builder.Property(u => u.OlusturulmaTarihi).IsRequired(false);
            builder.Property(u => u.GuncelleyenPersonelId).IsRequired(false);
            builder.Property(u => u.GuncellenmeTarihi).IsRequired(false);
            builder.Property(u => u.SilenPersonelId).IsRequired(false);
            builder.Property(u => u.SilinmeTarihi).IsRequired(false);
            builder.Property(r => r.DurumId).HasColumnType("SMALLINT").IsRequired();
            // Relationship
            builder.HasOne(u => u.Personel).WithOne(p => p.AppUser).HasForeignKey<AppUser>(u => u.PersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.OlusturanPersonel).WithMany().HasForeignKey(u => u.OlusturanPersonelId) .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.GuncelleyenPersonel).WithMany().HasForeignKey(u => u.GuncelleyenPersonelId) .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.SilenPersonel).WithMany().HasForeignKey(u => u.SilenPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.Durum).WithMany().HasForeignKey(u => u.DurumId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
