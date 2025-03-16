using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class MalzemeModelConfiguration : IEntityTypeConfiguration<MalzemeModel>
    {
        public void Configure(EntityTypeBuilder<MalzemeModel> builder)
        {
            builder.ToTable("MalzemeModel");

            builder.HasKey(x => x.MalzemeModelID);
            builder.Property(x => x.ModelAdi).HasMaxLength(25).ValueGeneratedOnAdd().IsRequired().IsUnicode(false);

            builder.HasOne(x => x.MalzemeMarka)
                   .WithMany(x => x.MalzemeModels)
                   .HasForeignKey(x => x.MalzemeMarkaId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.MalzemeCinsi)
            //       .WithMany(x => x.MalzemeModels)
            //       .HasForeignKey(x => x.MalzemeCinsiId)
            //       .OnDelete(DeleteBehavior.Restrict);

            var config = new BaseEntityConfiguration<MalzemeModel>();
            config.Configure(builder);
        }
    }

}
