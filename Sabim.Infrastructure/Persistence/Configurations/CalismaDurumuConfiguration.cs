using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class CalismaDurumuConfiguration : IEntityTypeConfiguration<CalismaDurumu>
    {
        public void Configure(EntityTypeBuilder<CalismaDurumu> builder)
        {
            builder.HasKey(c => c.CalismaDurumuID);
            builder.Property(c => c.CalismaDurumuID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(c => c.CalismaDurumAdi).HasMaxLength(85).IsRequired().IsUnicode();
            builder.HasIndex(c => c.CalismaDurumAdi).IsUnique();
            builder.Property(c => c.KurumPersonelListesineDahilMi).HasDefaultValue(true);
            //builder.HasData(
            //    new CalismaDurumu { CalismaDurumuID=1, CalismaDurumAdi= "Görevde", DurumId=1 },
            //    new CalismaDurumu { CalismaDurumuID=2, CalismaDurumAdi= "Geçici Ayrılış (Ücretsiz İzin, Askerlik,Doğum İzni vb.)", DurumId=1 },
            //    new CalismaDurumu { CalismaDurumuID=3, CalismaDurumAdi= "Kalıcı Ayrılış (Emeklilik, Nakil, İstifa,Görevlendirme Sonlandırılması vb.)", DurumId=1 }
            //);
            //Relationship
            builder.HasMany(c => c.Personels).WithOne(p => p.CalismaDurumu).HasForeignKey(c => c.CalismaDurumuId).OnDelete(DeleteBehavior.Restrict);
            var baseConfig = new BaseEntityConfiguration<CalismaDurumu>();
            baseConfig.Configure(builder);
        }
    }
}
