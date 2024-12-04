using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class GorevlendirilmeTuruConfiguration : IEntityTypeConfiguration<GorevlendirilmeTuru>
    {
        public void Configure(EntityTypeBuilder<GorevlendirilmeTuru> builder)
        {
            builder.HasKey(g => g.GorevlendirilmeTuruID);
            builder.Property(g => g.GorevlendirilmeTuruID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(g => g.GorevlendirilmeTuruAdi).HasMaxLength(50).IsRequired().IsUnicode();
            builder.HasIndex(g => g.GorevlendirilmeTuruAdi).IsUnique();
            builder.Property(g => g.KurumPersonelListesineDahilMi).HasDefaultValue(true);
            builder.HasData(
                    new GorevlendirilmeTuru { GorevlendirilmeTuruID = 1, GorevlendirilmeTuruAdi = "Görevlendirilmemiş", KurumPersonelListesineDahilMi = true, DurumId = 1 },
                    new GorevlendirilmeTuru { GorevlendirilmeTuruID = 2, GorevlendirilmeTuruAdi = "Dış Kurumdan Görevlendirilme", KurumPersonelListesineDahilMi = true, DurumId = 1 },
                    new GorevlendirilmeTuru { GorevlendirilmeTuruID = 3, GorevlendirilmeTuruAdi = "Dış Kuruma Görevlendirilme", KurumPersonelListesineDahilMi = true, DurumId = 1 }
                   );
            //Relationship
            builder.HasMany(g => g.Personels).WithOne(p => p.GorevlendirilmeTuru).HasForeignKey(p => p.GorevlendirilmeTuruId).OnDelete(DeleteBehavior.Restrict);
            var baseConfig = new BaseEntityConfiguration<GorevlendirilmeTuru>();
            baseConfig.Configure(builder);
        }
    }
}
