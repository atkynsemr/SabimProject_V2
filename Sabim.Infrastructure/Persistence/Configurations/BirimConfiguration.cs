using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class BirimConfiguration : IEntityTypeConfiguration<Birim>
    {
        public void Configure(EntityTypeBuilder<Birim> builder)
        {
            builder.HasKey(b => b.BirimID);
            builder.Property(b => b.BirimID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.BirimAdi).IsRequired().HasMaxLength(75).IsUnicode();
            builder.HasIndex(b => b.BirimAdi).IsUnique();
            //Relationship
            builder.HasOne(b => b.Bolum).WithMany(b => b.Birims).HasForeignKey(b => b.BolumId);
            builder.HasMany(b => b.Kisims).WithOne(k => k.Birim).HasForeignKey(k => k.BirimId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<Birim>();
            config.Configure(builder);
        }
    }
}
