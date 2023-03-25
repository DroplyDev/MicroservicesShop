﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain;
using ProductService.Infrastructure.Database;

#nullable disable

namespace ProductService.Infrastructure.Database.Configurations
{
    public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07791F3AAA");

            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.HasIndex(e => e.Name, "ix_name");

            entity.Property(e => e.Description)
            .HasMaxLength(500)
            .IsUnicode(false);
            entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Thumbnail).HasColumnType("image");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Products__Catego__267ABA7A");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> entity);
    }
}