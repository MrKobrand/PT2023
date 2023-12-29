using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CarDetailConfiguration : IEntityTypeConfiguration<CarDetail>
{
    public void Configure(EntityTypeBuilder<CarDetail> builder)
    {
        builder.Property(cd => cd.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(cd => cd.Price)
            .IsRequired();
    }
}
