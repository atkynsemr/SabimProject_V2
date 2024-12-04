using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class SehirConfiguration : IEntityTypeConfiguration<Sehir>
    {
        public void Configure(EntityTypeBuilder<Sehir> builder)
        {
            builder.HasKey(s => s.SehirID);
            builder.Property(s => s.SehirID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(s => s.SehirAdi).HasMaxLength(15).IsRequired().IsUnicode();
            builder.HasIndex(s => s.SehirAdi).IsUnique();
            builder.Property(s => s.SehirKodu).HasColumnType("SMALLINT").IsRequired();
            builder.HasIndex(s => s.SehirKodu).IsUnique();
            builder.HasData(
                    new Sehir { SehirID = 1, SehirAdi = "Adana", SehirKodu = 1, DurumId = 1 },
                    new Sehir { SehirID = 2, SehirAdi = "Adıyaman", SehirKodu = 2, DurumId = 1 },
                    new Sehir { SehirID = 3, SehirAdi = "Afyonkarahisar", SehirKodu = 3, DurumId = 1 },
                    new Sehir { SehirID = 4, SehirAdi = "Ağrı", SehirKodu = 4, DurumId = 1 },
                    new Sehir { SehirID = 5, SehirAdi = "Amasya", SehirKodu = 5, DurumId = 1 },
                    new Sehir { SehirID = 6, SehirAdi = "Ankara", SehirKodu = 6, DurumId = 1 },
                    new Sehir { SehirID = 7, SehirAdi = "Antalya", SehirKodu = 7, DurumId = 1 },
                    new Sehir { SehirID = 8, SehirAdi = "Artvin", SehirKodu = 8, DurumId = 1 },
                    new Sehir { SehirID = 9, SehirAdi = "Aydın", SehirKodu = 9, DurumId = 1 },
                    new Sehir { SehirID = 10, SehirAdi = "Balıkesir", SehirKodu = 10, DurumId = 1 },
                    new Sehir { SehirID = 11, SehirAdi = "Bilecik", SehirKodu = 11, DurumId = 1 },
                    new Sehir { SehirID = 12, SehirAdi = "Bingöl", SehirKodu = 12, DurumId = 1 },
                    new Sehir { SehirID = 13, SehirAdi = "Bitlis", SehirKodu = 13, DurumId = 1 },
                    new Sehir { SehirID = 14, SehirAdi = "Bolu", SehirKodu = 14, DurumId = 1 },
                    new Sehir { SehirID = 15, SehirAdi = "Burdur", SehirKodu = 15, DurumId = 1 },
                    new Sehir { SehirID = 16, SehirAdi = "Bursa", SehirKodu = 16, DurumId = 1 },
                    new Sehir { SehirID = 17, SehirAdi = "Çanakkale", SehirKodu = 17, DurumId = 1 },
                    new Sehir { SehirID = 18, SehirAdi = "Çankırı", SehirKodu = 18, DurumId = 1 },
                    new Sehir { SehirID = 19, SehirAdi = "Çorum", SehirKodu = 19, DurumId = 1 },
                    new Sehir { SehirID = 20, SehirAdi = "Denizli", SehirKodu = 20, DurumId = 1 },
                    new Sehir { SehirID = 21, SehirAdi = "Diyarbakır", SehirKodu = 21, DurumId = 1 },
                    new Sehir { SehirID = 22, SehirAdi = "Edirne", SehirKodu = 22, DurumId = 1 },
                    new Sehir { SehirID = 23, SehirAdi = "Elazığ", SehirKodu = 23, DurumId = 1 },
                    new Sehir { SehirID = 24, SehirAdi = "Erzincan", SehirKodu = 24, DurumId = 1 },
                    new Sehir { SehirID = 25, SehirAdi = "Erzurum", SehirKodu = 25, DurumId = 1 },
                    new Sehir { SehirID = 26, SehirAdi = "Eskişehir", SehirKodu = 26, DurumId = 1 },
                    new Sehir { SehirID = 27, SehirAdi = "Gaziantep", SehirKodu = 27, DurumId = 1 },
                    new Sehir { SehirID = 28, SehirAdi = "Giresun", SehirKodu = 28, DurumId = 1 },
                    new Sehir { SehirID = 29, SehirAdi = "Gümüşhane", SehirKodu = 29, DurumId = 1 },
                    new Sehir { SehirID = 30, SehirAdi = "Hakkari", SehirKodu = 30, DurumId = 1 },
                    new Sehir { SehirID = 31, SehirAdi = "Hatay", SehirKodu = 31, DurumId = 1 },
                    new Sehir { SehirID = 32, SehirAdi = "Isparta", SehirKodu = 32, DurumId = 1 },
                    new Sehir { SehirID = 33, SehirAdi = "Mersin", SehirKodu = 33, DurumId = 1 },
                    new Sehir { SehirID = 34, SehirAdi = "İstanbul", SehirKodu = 34, DurumId = 1 },
                    new Sehir { SehirID = 35, SehirAdi = "İzmir", SehirKodu = 35, DurumId = 1 },
                    new Sehir { SehirID = 36, SehirAdi = "Kars", SehirKodu = 36, DurumId = 1 },
                    new Sehir { SehirID = 37, SehirAdi = "Kastamonu", SehirKodu = 37, DurumId = 1 },
                    new Sehir { SehirID = 38, SehirAdi = "Kayseri", SehirKodu = 38, DurumId = 1 },
                    new Sehir { SehirID = 39, SehirAdi = "Kırklareli", SehirKodu = 39, DurumId = 1 },
                    new Sehir { SehirID = 40, SehirAdi = "Kırşehir", SehirKodu = 40, DurumId = 1 },
                    new Sehir { SehirID = 41, SehirAdi = "Kocaeli", SehirKodu = 41, DurumId = 1 },
                    new Sehir { SehirID = 42, SehirAdi = "Konya", SehirKodu = 42, DurumId = 1 },
                    new Sehir { SehirID = 43, SehirAdi = "Kütahya", SehirKodu = 43, DurumId = 1 },
                    new Sehir { SehirID = 44, SehirAdi = "Malatya", SehirKodu = 44, DurumId = 1 },
                    new Sehir { SehirID = 45, SehirAdi = "Manisa", SehirKodu = 45, DurumId = 1 },
                    new Sehir { SehirID = 46, SehirAdi = "Kahramanmaraş", SehirKodu = 46, DurumId = 1 },
                    new Sehir { SehirID = 47, SehirAdi = "Mardin", SehirKodu = 47, DurumId = 1 },
                    new Sehir { SehirID = 48, SehirAdi = "Muğla", SehirKodu = 48, DurumId = 1 },
                    new Sehir { SehirID = 49, SehirAdi = "Muş", SehirKodu = 49, DurumId = 1 },
                    new Sehir { SehirID = 50, SehirAdi = "Nevşehir", SehirKodu = 50, DurumId = 1 },
                    new Sehir { SehirID = 51, SehirAdi = "Niğde", SehirKodu = 51, DurumId = 1 },
                    new Sehir { SehirID = 52, SehirAdi = "Ordu", SehirKodu = 52, DurumId = 1 },
                    new Sehir { SehirID = 53, SehirAdi = "Rize", SehirKodu = 53, DurumId = 1 },
                    new Sehir { SehirID = 54, SehirAdi = "Sakarya", SehirKodu = 54, DurumId = 1 },
                    new Sehir { SehirID = 55, SehirAdi = "Samsun", SehirKodu = 55, DurumId = 1 },
                    new Sehir { SehirID = 56, SehirAdi = "Siirt", SehirKodu = 56, DurumId = 1 },
                    new Sehir { SehirID = 57, SehirAdi = "Sinop", SehirKodu = 57, DurumId = 1 },
                    new Sehir { SehirID = 58, SehirAdi = "Sivas", SehirKodu = 58, DurumId = 1 },
                    new Sehir { SehirID = 59, SehirAdi = "Tekirdağ", SehirKodu = 59, DurumId = 1 },
                    new Sehir { SehirID = 60, SehirAdi = "Tokat", SehirKodu = 60, DurumId = 1 },
                    new Sehir { SehirID = 61, SehirAdi = "Trabzon", SehirKodu = 61, DurumId = 1 },
                    new Sehir { SehirID = 62, SehirAdi = "Tunceli", SehirKodu = 62, DurumId = 1 },
                    new Sehir { SehirID = 63, SehirAdi = "Şanlıurfa", SehirKodu = 63, DurumId = 1 },
                    new Sehir { SehirID = 64, SehirAdi = "Uşak", SehirKodu = 64, DurumId = 1 },
                    new Sehir { SehirID = 65, SehirAdi = "Van", SehirKodu = 65, DurumId = 1 },
                    new Sehir { SehirID = 66, SehirAdi = "Yozgat", SehirKodu = 66, DurumId = 1 },
                    new Sehir { SehirID = 67, SehirAdi = "Zonguldak", SehirKodu = 67, DurumId = 1 },
                    new Sehir { SehirID = 68, SehirAdi = "Aksaray", SehirKodu = 68, DurumId = 1 },
                    new Sehir { SehirID = 69, SehirAdi = "Bayburt", SehirKodu = 69, DurumId = 1 },
                    new Sehir { SehirID = 70, SehirAdi = "Karaman", SehirKodu = 70, DurumId = 1 },
                    new Sehir { SehirID = 71, SehirAdi = "Kırıkkale", SehirKodu = 71, DurumId = 1 },
                    new Sehir { SehirID = 72, SehirAdi = "Batman", SehirKodu = 72, DurumId = 1 },
                    new Sehir { SehirID = 73, SehirAdi = "Şırnak", SehirKodu = 73, DurumId = 1 },
                    new Sehir { SehirID = 74, SehirAdi = "Bartın", SehirKodu = 74, DurumId = 1 },
                    new Sehir { SehirID = 75, SehirAdi = "Ardahan", SehirKodu = 75, DurumId = 1 },
                    new Sehir { SehirID = 76, SehirAdi = "Iğdır", SehirKodu = 76, DurumId = 1 },
                    new Sehir { SehirID = 77, SehirAdi = "Yalova", SehirKodu = 77, DurumId = 1 },
                    new Sehir { SehirID = 78, SehirAdi = "Karabük", SehirKodu = 78, DurumId = 1 },
                    new Sehir { SehirID = 79, SehirAdi = "Kilis", SehirKodu = 79, DurumId = 1 },
                    new Sehir { SehirID = 80, SehirAdi = "Osmaniye", SehirKodu = 80, DurumId = 1 },
                    new Sehir { SehirID = 81, SehirAdi = "Düzce", SehirKodu = 81, DurumId = 1 }
                );
            //Relationship
            builder.HasMany(k => k.Kurums).WithOne(s => s.Sehir).HasForeignKey(k => k.SehirId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<Sehir>();
            config.Configure(builder);
        }
    }
}
