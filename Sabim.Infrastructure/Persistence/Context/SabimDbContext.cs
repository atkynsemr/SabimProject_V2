using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Context
{
    public class SabimDbContext : IdentityDbContext<
        AppUser,
        AppRole,
        int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        AppRoleClaim,
        IdentityUserToken<int>>
        {
        public SabimDbContext(DbContextOptions<SabimDbContext> options) : base(options)
        {

        }
        public DbSet<Durum> Durum { get; set; }
        public DbSet<CalismaDurumu> CalismaDurumu { get; set; }
        public DbSet<Cinsiyet> Cinsiyet{ get; set; }
        public DbSet<KanGrubu> KanGrubu { get; set; }
        public DbSet<GorevlendirilmeTuru> GorevlendirilmeTuru { get; set; }
        public DbSet<KadroTuru> KadroTuru { get; set; }
        public DbSet<Kurum> Kurum { get; set; }
        public DbSet<KurumTipi> KurumTipi { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Sehir> Sehir { get; set; }
        public DbSet<Unvan> Unvan { get; set; }
        public DbSet<Bolum> Bolum { get; set; }
        public DbSet<Birim> Birim { get; set; }
        public DbSet<Kisim> Kisim { get; set; }
        public DbSet<KabinetBazliBolum> KabinetBazliBolum { get; set; }
        public DbSet<GorevlendirilmeTipi> GorevlendirilmeTipi { get; set; }
        public DbSet<PersonelGorevlendirilme> PersonelGorevlendirilme { get; set; }
        public DbSet<PersonelUnvanGecmisi> PersonelUnvanGecmisi { get; set; }   
        public DbSet<SavciCalisilanKatip> SavciCalisilanKatip { get; set; }
        public DbSet<SidebarMenu> SidebarMenu { get; set; }
        public DbSet<Ekran> Ekran { get; set; }
        public DbSet<AppRoleClaim> AppRoleClaims { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SabimDbContext).Assembly);
        }
    }
}
