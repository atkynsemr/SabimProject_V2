using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class KabinetBazliBolumConfiguration : IEntityTypeConfiguration<KabinetBazliBolum>
    {
        public void Configure(EntityTypeBuilder<KabinetBazliBolum> builder)
        {
            builder.HasKey(k => k.KabinetBazliBolumID);
            builder.Property(k => k.KabinetBazliBolumID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(k => k.KabinetBazliBolumAdi).IsRequired().HasMaxLength(35).IsUnicode();
            builder.HasIndex(k => k.KabinetBazliBolumAdi).IsUnique();
            //Relationship
            builder.HasMany(k => k.Kisims).WithOne(k => k.KabinetBazliBolum).HasForeignKey(k => k.KabinetBazliBolumId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<KabinetBazliBolum>();
            config.Configure(builder);
        }
    }
}
