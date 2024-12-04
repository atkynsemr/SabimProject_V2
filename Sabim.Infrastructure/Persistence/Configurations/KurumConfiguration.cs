using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class KurumConfiguration : IEntityTypeConfiguration<Kurum>
    {
        public void Configure(EntityTypeBuilder<Kurum> builder)
        {
            builder.HasKey(k => k.KurumID);
            builder.Property(k => k.KurumID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(k => k.KurumAdi).HasMaxLength(100).IsRequired().IsUnicode();
            builder.HasIndex(k => new { k.KurumAdi, k.SehirId }).IsUnique();
            //Relationship
            builder.HasOne(k => k.KurumTipi).WithMany(kt => kt.Kurums).HasForeignKey(k =>k.KurumTipiId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(k => k.Sehir).WithMany(s => s.Kurums).HasForeignKey(k => k.SehirId).OnDelete(DeleteBehavior.Restrict);
            var config= new BaseEntityConfiguration<Kurum>();
            config.Configure(builder);
        }
    }
}
