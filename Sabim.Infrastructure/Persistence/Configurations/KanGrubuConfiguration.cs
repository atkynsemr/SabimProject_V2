using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class KanGrubuConfiguration : IEntityTypeConfiguration<KanGrubu>
    {
        public void Configure(EntityTypeBuilder<KanGrubu> builder)
        {
            builder.HasKey(kg => kg.KanGrubuID);
            builder.Property(kg => kg.KanGrubuID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(kg => kg.KanGrubuAdi).HasMaxLength(10).IsRequired().IsUnicode(false);
            builder.HasData(
                new KanGrubu { KanGrubuID = 1, KanGrubuAdi = "0 Rh(-)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 2, KanGrubuAdi = "0 Rh(+)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 3, KanGrubuAdi = "A Rh(-)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 4, KanGrubuAdi = "A Rh(+)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 5, KanGrubuAdi = "AB Rh(-)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 6, KanGrubuAdi = "AB Rh(+)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 7, KanGrubuAdi = "B Rh(-)", DurumId = 1, OlusturulmaTarihi = DateTime.Now },
                new KanGrubu { KanGrubuID = 8, KanGrubuAdi = "B Rh(+)", DurumId = 1, OlusturulmaTarihi = DateTime.Now }
                );
            //Relationship
            builder.HasMany(p => p.Personels).WithOne(kg => kg.KanGrubu).HasForeignKey(p => p.KanGrubuId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<KanGrubu>();
            config.Configure(builder);
        }
    }
}
