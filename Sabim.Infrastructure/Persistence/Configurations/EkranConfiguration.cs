using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class EkranConfiguration : IEntityTypeConfiguration<Ekran>
    {
        public void Configure(EntityTypeBuilder<Ekran> builder)
        {
            builder.HasKey(e => e.EkranID);
            builder.Property(e => e.EkranID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(e => e.EkranAdi).HasMaxLength(100).IsUnicode().IsRequired();
            builder.HasIndex(e => e.EkranAdi).IsUnique();
            builder.Property(e => e.Varsayilan).HasDefaultValue(false);
            //Relationship
            builder.HasOne(e => e.SidebarMenu).WithMany(sm => sm.Ekrans).HasForeignKey(e => e.SidebarMenuId).OnDelete(DeleteBehavior.Restrict);  
            builder.HasMany(e => e.AppRoleClaims).WithOne(rey => rey.Ekran).HasForeignKey(rey => rey.EkranId).OnDelete(DeleteBehavior.Restrict);  
            var config = new BaseEntityConfiguration<Ekran>();
            config.Configure(builder);
        }
    }
}
