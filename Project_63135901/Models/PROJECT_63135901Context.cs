using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }

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
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.AccPassword).HasMaxLength(50);

                entity.Property(e => e.Salt)
                    .HasMaxLength(6)
                    .IsFixedLength(true);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Accounts__RoleID__68487DD7");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("PK__Categori__6A1C8ADACB84C33A");

                entity.Property(e => e.CatId)
                    .ValueGeneratedNever()
                    .HasColumnName("CatID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CatDescription).HasColumnType("text");

                entity.Property(e => e.CatName).HasMaxLength(250);

                entity.Property(e => e.Cover)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ShcemaMarkup).HasColumnType("text");

                entity.Property(e => e.Thumb).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomersId)
                    .HasName("PK__Customer__EB5B581EF3F86FAB");

                entity.Property(e => e.CustomersId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomersID");

                entity.Property(e => e.AccPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(6)
                    .IsFixedLength(true);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CusAddress).HasMaxLength(255);

                entity.Property(e => e.District).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ward).HasMaxLength(150);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Customers__Locat__6B24EA82");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId)
                    .ValueGeneratedNever()
                    .HasColumnName("LocationID");

                entity.Property(e => e.LocationName).HasMaxLength(50);

                entity.Property(e => e.LocationType).HasMaxLength(50);

                entity.Property(e => e.NameWithType).HasMaxLength(100);

                entity.Property(e => e.PathWithType).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.CustomersId).HasColumnName("CustomersID");

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.TransactStatusId).HasColumnName("TransactStatusID");

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomersId)
                    .HasConstraintName("FK__Orders__Customer__6E01572D");

                entity.HasOne(d => d.TransactStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactStatusId)
                    .HasConstraintName("FK__Orders__Transact__6EF57B66");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__787EE5A0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__797309D9");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => e.PagesId)
                    .HasName("PK__Pages__D73F818DA56BB11D");

                entity.Property(e => e.PagesId)
                    .ValueGeneratedNever()
                    .HasColumnName("PagesID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.Contents).HasColumnType("text");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.PageName).HasMaxLength(250);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.Alias).HasMaxLength(250);

                entity.Property(e => e.CatId).HasColumnName("CatID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MetaDesc).HasMaxLength(250);

                entity.Property(e => e.MetaKey).HasMaxLength(250);

                entity.Property(e => e.ProductDescription).HasColumnType("text");

                entity.Property(e => e.ProductName).HasMaxLength(250);

                entity.Property(e => e.ShortDescription).HasMaxLength(250);

                entity.Property(e => e.Tags).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.Video).HasMaxLength(255);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK__Products__CatID__75A278F5");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleDescription).HasColumnType("text");

                entity.Property(e => e.RoleName).HasMaxLength(250);
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.Property(e => e.ShipperId)
                    .ValueGeneratedNever()
                    .HasColumnName("ShipperID");

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
                    .HasName("PK__Transact__C8BCD2768AEC694A");

                entity.ToTable("TransactionStatus");

                entity.Property(e => e.TransactStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("TransactStatusID");

                entity.Property(e => e.TransStatus).HasMaxLength(50);

                entity.Property(e => e.TransactDescription).HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
