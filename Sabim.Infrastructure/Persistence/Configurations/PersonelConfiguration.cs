using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class PersonelConfiguration : IEntityTypeConfiguration<Personel>
    {
        public void Configure(EntityTypeBuilder<Personel> builder)
        {
            builder.HasKey(p => p.PersonelID);
            builder.Property(p => p.PersonelID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Ad).HasMaxLength(30).IsRequired().IsUnicode();
            builder.Property(p => p.Soyad).HasMaxLength(25).IsRequired().IsUnicode();
            builder.Property(p => p.SicilNumarasi).IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("CK_Personel_SicilNumarasi", "[SicilNumarasi] BETWEEN 10000 AND 9999999"));
            builder.Property(p => p.CepTelefonu).HasMaxLength(11).IsUnicode(false);
            builder.Property(p => p.CinsiyetId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(p => p.KanGrubuId).HasColumnType("SMALLINT");
            builder.Property(p => p.DogumYeri).HasMaxLength(35).IsUnicode();
            builder.Property(p => p.Derece).HasColumnType("SMALLINT");
            builder.ToTable(t => t.HasCheckConstraint("CK_Personel_Derece", "[Derece] BETWEEN 1 AND 15"));
            builder.Property(p => p.Kademe).HasColumnType("SMALLINT");
            builder.ToTable(t => t.HasCheckConstraint("CK_Personel_Kademe", "[Kademe] BETWEEN 1 AND 4"));
            builder.Property(p => p.KimlikNo).HasMaxLength(11).IsUnicode(false);
            builder.Property(p => p.AracPlakasi).HasMaxLength(11).IsUnicode(false);
            //Relationship
            builder.HasOne(p => p.Cinsiyet).WithMany(c => c.Personels).HasForeignKey(p => p.CinsiyetId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.KanGrubu).WithMany(c => c.Personels).HasForeignKey(p => p.KanGrubuId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.CalismaDurumu).WithMany(c => c.Personels).HasForeignKey(p => p.CalismaDurumuId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.GorevlendirilmeTuru).WithMany(g => g.Personels).HasForeignKey(p => p.GorevlendirilmeTuruId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.KadroTuru).WithMany(k => k.Personels).HasForeignKey(p => p.KadroTuruId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Kurum).WithMany(k => k.Personels).HasForeignKey(p => p.KurumId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Unvan).WithMany(k => k.Personels).HasForeignKey(p => p.UnvanId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.PersonelGorevlendirilmes).WithOne(pg => pg.Personel).HasForeignKey(pg => pg.PersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.AppUser).WithOne(au => au.Personel).HasForeignKey<AppUser>(au => au.PersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.PersonelUnvanGecmisis).WithOne(pu => pu.Personel).HasForeignKey(pu => pu.PersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.SavciOlarakCalisilanKatips) // Savcı olarak çalıştığı kayıtlar
                .WithOne(sck => sck.Savci) // Savcı olarak yer alan Personel ile ilişkilendir
                .HasForeignKey(sck => sck.SavciId) // SavcıId üzerinden ilişkiyi tanımlar
                .OnDelete(DeleteBehavior.Cascade); // Eğer Personel (Savcı) silinirse, ilgili kayıtları da sil

            builder.HasMany(p => p.KatipOlarakCalisilanKatips) // Katip olarak çalıştığı kayıtlar
                .WithOne(sck => sck.Katip) // Katip olarak yer alan Personel ile ilişkilendir
                .HasForeignKey(sck => sck.KatipId) // KatipId üzerinden ilişkiyi tanımlar
                .OnDelete(DeleteBehavior.NoAction); // Eğer Personel (Katip) silinirse, ilgili kayıtları da sil

            builder.HasMany(p => p.PersonelGeciciGorevlendirilmes).WithOne(pg => pg.Personel).HasForeignKey(pg => pg.PersonelId).OnDelete(DeleteBehavior.Restrict);

            var config = new BaseEntityConfiguration<Personel>();
            config.Configure(builder);
        }
    }
}
