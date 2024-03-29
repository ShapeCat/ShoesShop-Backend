﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.EntityConfigurations
{
    internal class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("models");
            builder.HasKey(x => x.ModelId);
            builder.Property(x => x.Name)
                   .HasMaxLength(255)
                   .IsRequired();
            builder.Property(x => x.Color)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.Brand)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(x => x.SkuId)
                   .HasMaxLength(255);
            builder.Property(x => x.ReleaseDate);
        }
    }
}
