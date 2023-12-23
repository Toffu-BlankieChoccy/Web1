using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_63135901.Models
{
    public partial class PROJECT_63135901Context : DbContext
    {
        public PROJECT_63135901Context()
        {
        }

        public PROJECT_63135901Context(DbContextOptions<PROJECT_63135901Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = LAPTOP-UGEFPH5D; Database = PROJECT_63135901; Integrated Security = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccPassword).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Accounts__RoleID__4316F928");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__Categori__6A1C8ADAA5CEB871");

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CatDescription).HasMaxLength(255);

                entity.Property(e => e.CatName).HasMaxLength(250);

                entity.Property(e => e.Cover).HasMaxLength(255);

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ShcemaMarkup).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomersId)
                    .HasName("PK__Customer__EB5B581EDDFBFE66");

                entity.Property(e => e.CustomersId).HasColumnName("CustomersID");

                entity.Property(e => e.AccPassword).HasMaxLength(50);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CusAddress).HasMaxLength(255);

                entity.Property(e => e.District).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.Ward).HasMaxLength(150);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Customers__Locat__45F365D3");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LocationName).HasMaxLength(50);

                entity.Property(e => e.LocationType).HasMaxLength(50);

                entity.Property(e => e.NameWithType).HasMaxLength(100);

                entity.Property(e => e.PathWithType).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomersId).HasColumnName("CustomersID");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TotalMoney).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomersId)
                    .HasConstraintName("FK__Orders__Customer__48CFD27E");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .HasConstraintName("FK__Orders__Transact__49C3F6B7");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__5441852A");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__5535A963");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => e.PagesId)
                    .HasName("PK__Pages__D73F818D1E9DC054");

                entity.Property(e => e.PagesId).HasColumnName("PagesID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.Contents).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.PageName).HasMaxLength(250);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.ProductDescription).HasMaxLength(255);

                entity.Property(e => e.ProductName).HasMaxLength(250);

                entity.Property(e => e.ShortDescription).HasMaxLength(250);

                entity.Property(e => e.Tags).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.Video).HasMaxLength(255);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK__Products__CatID__5070F446");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleDescription).HasMaxLength(255);

                entity.Property(e => e.RoleName).HasMaxLength(250);
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.Company).HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipperName).HasMaxLength(250);
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.HasKey(e => e.TransactStatusId)
                    .HasName("PK__Transact__C8BCD27675188907");

                entity.ToTable("TransactionStatus");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.Property(e => e.TransStatus).HasMaxLength(50);

                entity.Property(e => e.TransactDescription).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
