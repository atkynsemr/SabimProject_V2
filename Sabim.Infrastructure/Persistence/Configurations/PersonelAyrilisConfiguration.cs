using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class PersonelAyrilisConfiguration : IEntityTypeConfiguration<PersonelAyrilis>
    {
        public void Configure(EntityTypeBuilder<PersonelAyrilis> builder)
        {
            builder.ToTable("PersonelAyrilis", t =>
            {
                t.HasCheckConstraint("CHK_PersonelAyrilis_Tarih", "BaslangicTarihi IS NOT NULL OR BitisTarihi IS NOT NULL");
            });
            builder.HasKey(p => p.PersonelAyrilisID);
            builder.Property(p => p.PersonelAyrilisID).HasColumnType("SMALLINT").UseIdentityColumn();
            builder.Property(p => p.PersonelId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(p => p.PersonelAyrilisNedenleriId).HasColumnType("TINYINT").IsRequired();
            builder.Property(p => p.BaslangicTarihi).HasColumnType("DATE");
            builder.Property(p => p.BitisTarihi).HasColumnType("DATE");
            builder.Property(p => p.PersonelAyrilisYeriId).HasColumnType("SMALLINT");
            // Foreign Key Relations
            builder.HasOne(p => p.Personel).WithMany(p => p.PersonelAyriliss).HasForeignKey(p => p.PersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.PersonelAyrilisNedenleri).WithMany(n => n.PersonelAyriliss).HasForeignKey(p => p.PersonelAyrilisNedenleriId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.PersonelAyrilisYeri).WithMany(k => k.PersonelAyriliss).HasForeignKey(p => p.PersonelAyrilisYeriId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<PersonelAyrilis>();
            config.Configure(builder);
        }
    }

}
