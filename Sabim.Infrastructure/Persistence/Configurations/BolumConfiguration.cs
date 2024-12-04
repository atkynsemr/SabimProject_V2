using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class BolumConfiguration : IEntityTypeConfiguration<Bolum>
    {
        public void Configure(EntityTypeBuilder<Bolum> builder)
        {
            builder.HasKey(b => b.BolumID);
            builder.Property(b => b.BolumID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.BolumAdi).IsRequired().HasMaxLength(50).IsUnicode();
            builder.HasIndex(b => b.BolumAdi).IsUnique();
            //Relationship
            builder.HasMany(b => b.Birims).WithOne(bi => bi.Bolum).HasForeignKey(bi => bi.BolumId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<Bolum>();
            config.Configure(builder);
        }
    }
}
