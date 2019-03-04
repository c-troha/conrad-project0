using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VideoGameOrderSystem.DataAccess
{
    public partial class OrderSystemContext : DbContext
    {
        public OrderSystemContext()
        {
        }

        public OrderSystemContext(DbContextOptions<OrderSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<IbundleItems> IbundleItems { get; set; }
        public virtual DbSet<Ibundles> Ibundles { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Iproduct> Iproduct { get; set; }
        public virtual DbSet<ObundleItems> ObundleItems { get; set; }
        public virtual DbSet<Obundles> Obundles { get; set; }
        public virtual DbSet<Oproduct> Oproduct { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_To_Store");
            });

            modelBuilder.Entity<IbundleItems>(entity =>
            {
                entity.HasKey(e => new { e.BundleId, e.ProductId });

                entity.ToTable("IBundleItems", "OS");

                entity.Property(e => e.BundleId).HasColumnName("BundleID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Bundle)
                    .WithMany(p => p.IbundleItems)
                    .HasForeignKey(d => d.BundleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BundleItems_To_IBundles");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.IbundleItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BundleItems_To_IProduct");
            });

            modelBuilder.Entity<Ibundles>(entity =>
            {
                entity.ToTable("IBundles", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId });

                entity.ToTable("Inventory", "OS");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_To_Product");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_To_Store");
            });

            modelBuilder.Entity<Iproduct>(entity =>
            {
                entity.ToTable("IProduct", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<ObundleItems>(entity =>
            {
                entity.HasKey(e => new { e.BundleId, e.ProductId });

                entity.ToTable("OBundleItems", "OS");

                entity.Property(e => e.BundleId).HasColumnName("BundleID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Bundle)
                    .WithMany(p => p.ObundleItems)
                    .HasForeignKey(d => d.BundleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BundleItems_To_OBundles");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ObundleItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BundleItems_To_OProduct");
            });

            modelBuilder.Entity<Obundles>(entity =>
            {
                entity.ToTable("OBundles", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Oproduct>(entity =>
            {
                entity.ToTable("OProduct", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("OrderItems", "OS");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_To_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_To_Product");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_To_Customer");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_To_Store");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "OS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
