using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WaterShop.Goods.Domain.Entities;
using WaterShop.Goods.Domain.ValueObjects;

namespace WaterShop.Goods.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(key => key.Identity);
        builder.Property(p => p.Identity).HasColumnName("identity").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasConversion(to => to.Value, from => ProductName.Create(from));

        builder.Property(p => p.IdentityBrand).HasColumnName("brand_id").IsRequired();
        builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(x => x.IdentityBrand);

        builder.Property(p => p.IdentityBatch).HasColumnName("batch_id").IsRequired();
        builder.HasOne(x => x.Batch).WithMany(x => x.Products).HasForeignKey(x => x.IdentityBatch);

        builder.Property(p => p.IdentityType).HasColumnName("type_id").IsRequired();
        builder.HasOne(x => x.Type).WithMany(x => x.Products).HasForeignKey(x => x.IdentityType);
    }
}
