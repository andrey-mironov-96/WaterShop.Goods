using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WaterShop.Goods.Domain.Entities;

namespace WaterShop.Goods.Infrastructure.Configurations;

internal class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.ToTable("brands").HasKey(k => k.Identity);
        builder.Property(p => p.Identity).HasColumnName("id").IsRequired();
        builder.Property(p => p.Value).HasColumnName("name").IsRequired();

    }
}
