using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class SidebarMenuConfiguration : IEntityTypeConfiguration<SidebarMenu>
    {
        public void Configure(EntityTypeBuilder<SidebarMenu> builder)
        {
            builder.HasKey(sm => sm.SidebarMenuID);
            builder.Property(sm => sm.SidebarMenuID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(sm => sm.SidebarMenuAdi).HasMaxLength(60).IsUnicode().IsRequired();
            builder.HasIndex(sm => sm.SidebarMenuAdi).IsUnique();
            //Relationship
            builder.HasMany(e => e.Ekrans).WithOne(sm => sm.SidebarMenu).HasForeignKey(e => e.SidebarMenuId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<SidebarMenu>();
            config.Configure(builder);
        }
    }
}
