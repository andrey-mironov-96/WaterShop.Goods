using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WaterShop.Goods.Domain.Entities;

namespace WaterShop.Goods.Infrastructure.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("product_types").HasKey(k => k.Identity);
            builder.Property(p => p.Identity).HasColumnName("id").IsRequired();
            builder.Property(p => p.Value).HasColumnName("value").IsRequired();
        }
    }
}
