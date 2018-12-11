using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BootShop.Models
{
    public partial class BootShopContext : DbContext
    {
        public virtual DbSet<TblBoots> TblBoots { get; set; }
        public virtual DbSet<TblCart> TblCart { get; set; }
        public virtual DbSet<TblColor> TblColor { get; set; }
        public virtual DbSet<TblInfor> TblInfor { get; set; }
        public virtual DbSet<TblOrder> TblOrder { get; set; }
        public virtual DbSet<TblOrderDetails> TblOrderDetails { get; set; }
        public virtual DbSet<TblSize> TblSize { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.;Database=BootShop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBoots>(entity =>
            {
                entity.ToTable("tbl_Boots");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Picture).HasMaxLength(50);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.TblBoots)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Boots_tbl_Color");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.TblBoots)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Boots_tbl_Size");
            });

            modelBuilder.Entity<TblCart>(entity =>
            {
                entity.HasKey(e => e.CartId);

                entity.ToTable("tbl_Cart");

                entity.Property(e => e.Picture).IsRequired();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblCart)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cart_tbl_Boots");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblCart)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cart_tbl_User");
            });

            modelBuilder.Entity<TblColor>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.ToTable("tbl_Color");

                entity.Property(e => e.ColorId).ValueGeneratedNever();

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblInfor>(entity =>
            {
                entity.ToTable("tbl_Infor");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("tbl_Order");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblOrder)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Order_tbl_User");
            });

            modelBuilder.Entity<TblOrderDetails>(entity =>
            {
                entity.ToTable("tbl_OrderDetails");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_OrderDetails_tbl_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_OrderDetails_tbl_Boots");
            });

            modelBuilder.Entity<TblSize>(entity =>
            {
                entity.HasKey(e => e.SizeId);

                entity.ToTable("tbl_Size");

                entity.Property(e => e.SizeId).ValueGeneratedNever();

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tbl_User");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
