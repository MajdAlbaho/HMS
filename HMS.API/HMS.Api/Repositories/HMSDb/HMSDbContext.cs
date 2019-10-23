using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HMS.Api.Repositories.HMSDb
{
    public partial class HMSDbContext : DbContext
    {
        public HMSDbContext()
        {
        }

        public HMSDbContext(DbContextOptions<HMSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<CompanyPhones> CompanyPhones { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<GroupPersons> GroupPersons { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<Nationalities> Nationalities { get; set; }
        public virtual DbSet<Needs> Needs { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<PhoneTypes> PhoneTypes { get; set; }
        public virtual DbSet<ReservationGroups> ReservationGroups { get; set; }
        public virtual DbSet<ReservationRooms> ReservationRooms { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<RoomNeeds> RoomNeeds { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=Majd-pc;Database=HMSDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Areas>(entity =>
            {
                entity.ToTable("Areas", "Location");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Discriminator).IsRequired();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("Cities", "Location");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.ToTable("Companies", "HR");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArAddress).HasMaxLength(500);

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnAddress).HasMaxLength(500);

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FriAddress).HasMaxLength(500);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.AreaId);
            });

            modelBuilder.Entity<CompanyPhones>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PhoneNumber })
                    .HasName("PK_CompanyPhones_CompanyId_PhoneNumber");

                entity.ToTable("CompanyPhones", "HR");

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyPhones)
                    .HasForeignKey(d => d.CompanyId);

                entity.HasOne(d => d.PhoneType)
                    .WithMany(p => p.CompanyPhones)
                    .HasForeignKey(d => d.PhoneTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.ToTable("Countries", "Location");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GroupPersons>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.PersonId })
                    .HasName("PK_GroupPersons_GroupId_PersonId");

                entity.ToTable("GroupPersons", "HR");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPersons)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupPersons_GroupId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.GroupPersons)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPersons_PersonId");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.ToTable("Groups", "HR");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CompanyId);
            });

            modelBuilder.Entity<Hotels>(entity =>
            {
                entity.ToTable("Hotels", "Hotel");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.Property(e => e.Rate).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Hotels_AreaId");
            });

            modelBuilder.Entity<Nationalities>(entity =>
            {
                entity.ToTable("Nationalities", "HR");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Needs>(entity =>
            {
                entity.ToTable("Needs", "Common");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.ToTable("Persons", "HR");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CopyOfIdentity).HasColumnType("image");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FatherArName).HasMaxLength(200);

                entity.Property(e => e.FatherEnName).HasMaxLength(200);

                entity.Property(e => e.FatherFriName).HasMaxLength(200);

                entity.Property(e => e.FirstArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FirstEnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FirstFriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IdNumber).HasMaxLength(100);

                entity.Property(e => e.LastArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastEnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastFriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MotherArName).HasMaxLength(200);

                entity.Property(e => e.MotherEnName).HasMaxLength(200);

                entity.Property(e => e.MotherFriName).HasMaxLength(200);

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PhoneTypes>(entity =>
            {
                entity.ToTable("PhoneTypes", "Common");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReservationGroups>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.GroupId })
                    .HasName("PK_ReservationGroups_ReservationId_GroupId");

                entity.ToTable("ReservationGroups", "Hotel");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ReservationGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationGroups)
                    .HasForeignKey(d => d.ReservationId);
            });

            modelBuilder.Entity<ReservationRooms>(entity =>
            {
                entity.ToTable("ReservationRooms", "Hotel");

                entity.HasIndex(e => new { e.ReservationId, e.RoomId, e.PersonId })
                    .HasName("UNIQUE_ReservationRooms_ReservationId_RoomId_PersonId")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ReservationRooms)
                    .HasForeignKey(d => d.PersonId);

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationRooms)
                    .HasForeignKey(d => d.ReservationId);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ReservationRooms)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.ToTable("Reservations", "Hotel");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.HotelId);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reservations_AspUsers_UserId");
            });

            modelBuilder.Entity<RoomNeeds>(entity =>
            {
                entity.HasKey(e => new { e.RoomId, e.NeedId })
                    .HasName("PK_RoomNeeds_RoomId_NeedId");

                entity.ToTable("RoomNeeds", "Hotel");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Need)
                    .WithMany(p => p.RoomNeeds)
                    .HasForeignKey(d => d.NeedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomNeeds_NeedId");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomNeeds)
                    .HasForeignKey(d => d.RoomId);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.ToTable("Rooms", "Hotel");

                entity.HasIndex(e => e.RoomNumber)
                    .HasName("Rooms_Unique_RoomNumber")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status", "Common");

                entity.Property(e => e.ArName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FriName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");
            });
        }
    }
}
