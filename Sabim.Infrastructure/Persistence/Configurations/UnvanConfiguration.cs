using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class UnvanConfiguration : IEntityTypeConfiguration<Unvan>
    {
        public void Configure(EntityTypeBuilder<Unvan> builder)
        {
            builder.HasKey(u => u.UnvanID);
            builder.Property(u => u.UnvanID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(u => u.UnvanAdi).HasMaxLength(60).IsUnicode().IsRequired();
            builder.HasIndex(u => u.UnvanAdi).IsUnique();
            builder.Property(u => u.OncelikSirasi).HasColumnType("SMALLINT").IsRequired();
            //Relationship
            builder.HasMany(p => p.Personels).WithOne(u => u.Unvan).HasForeignKey(p => p.UnvanId).OnDelete(DeleteBehavior.Restrict);
            var config= new BaseEntityConfiguration<Unvan>();
            config.Configure(builder);
        }
    }
}
