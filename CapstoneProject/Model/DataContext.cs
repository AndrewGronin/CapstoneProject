using CapstoneProject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CapstoneProject.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<PartRequest> PartRequests { get; set; }
        public virtual DbSet<ResponsibleEmployee> ResponsibleEmployees { get; set; }
        public virtual DbSet<ResponsibleEmployeesType> ResponsibleEmployeesTypes { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<SparePartApplication> SparePartApplications { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<PartRequest>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PartRequests_pk")
                    .IsClustered(false);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.IssuingDepartment)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasConversion<string>();
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("RefreshTokens_pk")
                    .IsClustered(false);

                entity.Property(e => e.CreatedByIp)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");

                entity.Property(e => e.RevocationDateTime).HasColumnType("datetime");

                entity.Property(e => e.RevokedByIp).HasMaxLength(50);

                entity.Property(e => e.Token).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("RefreshTokens_Users_Id_fk");
            });

            modelBuilder.Entity<ResponsibleEmployee>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ResponsibleEmployees_pk")
                    .IsClustered(false);

                entity.Property(e => e.ContactNumber).IsRequired();

                entity.Property(e => e.FullName).IsRequired();
            });

            modelBuilder.Entity<ResponsibleEmployeesType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ResponsibleEmployees_Types");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Employee)
                    .WithMany()
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ResponsibleEmployees_Types_ResponsibleEmployees_Id_fk");

                entity.HasOne(d => d.Type)
                    .WithMany()
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ResponsibleEmployees_Types_Types_Id_fk");
            });

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SpareParts_pk")
                    .IsClustered(false);

                entity.Property(e => e.Brand).IsRequired();

                entity.Property(e => e.Manufacturer).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.SpareParts)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SpareParts_Types_Id_fk");
            });

            modelBuilder.Entity<SparePartApplication>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SparePartApplications_pk")
                    .IsClustered(false);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.SparePartApplications)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("SparePartApplications_PartRequests_Id_fk");

                entity.HasOne(d => d.SparePart)
                    .WithMany(p => p.SparePartApplications)
                    .HasForeignKey(d => d.SparePartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SparePartApplications_SpareParts_Id_fk");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Types_pk")
                    .IsClustered(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Users_pk")
                    .IsClustered(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.PasswordHash).IsRequired();
            });
        }

    }
}
