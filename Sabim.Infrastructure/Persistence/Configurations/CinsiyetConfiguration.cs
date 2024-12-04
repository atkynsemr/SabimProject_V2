using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class CinsiyetConfiguration : IEntityTypeConfiguration<Cinsiyet>
    {
        public void Configure(EntityTypeBuilder<Cinsiyet> builder)
        {
            builder.HasKey(c => c.CinsiyetID);
            builder.Property(c => c.CinsiyetID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.CinsiyetAdi).HasMaxLength(15).IsRequired().IsUnicode();
            builder.HasData(
                new Cinsiyet { CinsiyetID = 1, CinsiyetAdi = "Erkek", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new Cinsiyet { CinsiyetID = 2, CinsiyetAdi = "Kadın", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new Cinsiyet { CinsiyetID = 3, CinsiyetAdi = "Belirtilmemiş", DurumId = 1, OlusturulmaTarihi = DateTime.Now }
               );
            //Relationship
            builder.HasMany(p => p.Personels).WithOne(c => c.Cinsiyet).HasForeignKey(p => p.CinsiyetId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<Cinsiyet>();
            config.Configure(builder);
        }
    }
}
