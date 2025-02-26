using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class PersonelAyrilisNedenleriConfiguration : IEntityTypeConfiguration<PersonelAyrilisNedenleri>
    {
        public void Configure(EntityTypeBuilder<PersonelAyrilisNedenleri> builder)
        {
            builder.ToTable("PersonelAyrilisNedenleri");
            builder.HasKey(p => p.PersonelAyrilisNedenleriID);
            builder.Property(p => p.PersonelAyrilisNedenleriID).HasColumnType("TINYINT").UseIdentityColumn();
            builder.Property(p => p.Aciklama).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(p => p.KaliciAyrilisMi).HasColumnType("BIT").HasDefaultValue(false);
            builder.Property(p => p.DonanimUyarisi).HasColumnType("BIT").HasDefaultValue(false);
            builder.HasMany(p => p.PersonelAyriliss).WithOne(a => a.PersonelAyrilisNedenleri).HasForeignKey(a => a.PersonelAyrilisNedenleriId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<PersonelAyrilisNedenleri>();
            config.Configure(builder);
        }
    }
}
