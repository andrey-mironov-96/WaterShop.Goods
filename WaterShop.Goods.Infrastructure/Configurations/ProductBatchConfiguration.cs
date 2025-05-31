using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WaterShop.Goods.Domain.Entities;

namespace WaterShop.Goods.Infrastructure.Configurations
{
    internal class ProductBatchConfiguration : IEntityTypeConfiguration<ProductBatch>
    {
        public void Configure(EntityTypeBuilder<ProductBatch> builder)
        {
            builder.ToTable("batches").HasKey(k => k.Identity);
            builder.Property(p => p.Identity).HasColumnName("id").IsRequired();
            builder.Property(p => p.Value).HasColumnName("name").IsRequired();
            builder.Property(p => p.CreateAt).HasColumnName("create_at").HasColumnType("timestamp without time zone").IsRequired();
        }
    }
}
