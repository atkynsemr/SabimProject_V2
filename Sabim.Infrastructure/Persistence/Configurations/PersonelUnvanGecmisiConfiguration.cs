using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class PersonelUnvanGecmisiConfiguration : IEntityTypeConfiguration<PersonelUnvanGecmisi>
    {
        public void Configure(EntityTypeBuilder<PersonelUnvanGecmisi> builder)
        {
            builder.HasKey(pu => pu.PersonelUnvanGecmisiID);
            builder.Property(pu => pu.PersonelUnvanGecmisiID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(pu => pu.PersonelId).IsRequired();
            builder.Property(pu => pu.UnvanId).IsRequired();
            builder.HasOne(pu => pu.Personel).WithMany(p => p.PersonelUnvanGecmisis).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pu => pu.Unvan).WithMany(u => u.PersonelUnvanGecmisis).HasForeignKey(pu => pu.UnvanId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<PersonelUnvanGecmisi>();
            config.Configure(builder);
        }
    }
}
