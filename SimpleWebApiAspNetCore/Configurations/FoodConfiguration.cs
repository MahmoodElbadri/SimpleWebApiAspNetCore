using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebApiAspNetCore.Entities;

namespace SimpleWebApiAspNetCore.Configurations;

public class FoodConfiguration:IEntityTypeConfiguration<FoodEntity>
{
    public void Configure(EntityTypeBuilder<FoodEntity> builder)
    {
        builder.HasKey(tmp => tmp.Id);
        builder.Property(tmp => tmp.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(tmp => tmp.Type)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(tmp=>tmp.Calories)
            .IsRequired();
        // builder.HasCheckConstraint("CK_Food_Calories", "Calories > 1000");
    }
}
