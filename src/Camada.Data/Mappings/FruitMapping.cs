using Fruits.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fruits.Data.Mappings
{
    public class FruitMapping: IEntityTypeConfiguration<Fruit>
    {
        public void Configure(EntityTypeBuilder<Fruit> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(f => f.Description)
                .HasColumnType("varchar(4000)");

            builder.Property(f => f.Image)
                .HasColumnType("varchar(100)");

            builder.ToTable("Fruits");
        }
    }
}