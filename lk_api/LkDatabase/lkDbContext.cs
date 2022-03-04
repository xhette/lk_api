using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

using lk_api.LkDatabase.Models;

namespace lk_api
{
    public partial class lkDbContext : DbContext
    {

        private readonly IConfiguration _configuration;
        private string _connectionString;

        public lkDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LkDbConnection");
        }

        public lkDbContext(DbContextOptions<lkDbContext> options,IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LkDbConnection");
        }

        public virtual DbSet<Abonent> Abonents { get; set; } = null!;
        public virtual DbSet<Accural> Accurals { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<DeviceType> DeviceTypes { get; set; } = null!;
        public virtual DbSet<Notiffication> Notiffications { get; set; } = null!;
        public virtual DbSet<Tariff> Tariffs { get; set; } = null!;
        public virtual DbSet<UsersTariff> UsersTariffs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abonent>(entity =>
            {
                entity.ToTable("abonents");

                entity.HasIndex(e => e.PersonalNumber, "abonents_personal_number_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(20)
                    .HasColumnName("patronymic")
                    .IsFixedLength();

                entity.Property(e => e.PersonalNumber)
                    .HasMaxLength(25)
                    .HasColumnName("personal_number")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.SendReceiptEmail)
                    .IsRequired()
                    .HasColumnName("send_receipt_email")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.SendReceiptPost)
                    .IsRequired()
                    .HasColumnName("send_receipt_post")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Accural>(entity =>
            {
                entity.ToTable("accurals");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AbonentId).HasColumnName("abonent_id");

                entity.Property(e => e.Debt).HasColumnName("debt");

                entity.Property(e => e.Fine).HasColumnName("fine");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.Prepayment).HasColumnName("prepayment");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Abonent)
                    .WithMany(p => p.Accurals)
                    .HasForeignKey(d => d.AbonentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accurals_abonent_id_fkey");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address")
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("devices");

                entity.HasIndex(e => e.DeviceNumber, "devices_device_number_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AbonentId).HasColumnName("abonent_id");

                entity.Property(e => e.DeviceNumber)
                    .HasMaxLength(20)
                    .HasColumnName("device_number")
                    .IsFixedLength();

                entity.Property(e => e.IndicationDate).HasColumnName("indication_date");

                entity.Property(e => e.LastIndication).HasColumnName("last_indication");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.VerificationPeriod).HasColumnName("verification_period");

                entity.HasOne(d => d.Abonent)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.AbonentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("devices_abonent_id_fkey");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("devices_type_fkey");
            });

            modelBuilder.Entity<DeviceType>(entity =>
            {
                entity.ToTable("device_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .HasColumnName("type_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Notiffication>(entity =>
            {
                entity.ToTable("notiffications");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AbonentId).HasColumnName("abonent_id");

                entity.Property(e => e.NotifficationDate).HasColumnName("notiffication_date");

                entity.Property(e => e.NotifficationTime).HasColumnName("notiffication_time");

                entity.Property(e => e.ReadStatus).HasColumnName("read_status");

                entity.Property(e => e.Text)
                    .HasMaxLength(200)
                    .HasColumnName("text")
                    .IsFixedLength();

                entity.HasOne(d => d.Abonent)
                    .WithMany(p => p.Notiffications)
                    .HasForeignKey(d => d.AbonentId)
                    .HasConstraintName("notiffications_abonent_id_fkey");
            });

            modelBuilder.Entity<Tariff>(entity =>
            {
                entity.ToTable("tariffs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.TariffName)
                    .HasMaxLength(35)
                    .HasColumnName("tariff_name")
                    .IsFixedLength();

                entity.Property(e => e.Unit)
                    .HasMaxLength(15)
                    .HasColumnName("unit")
                    .IsFixedLength();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Tariffs)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tariffs_company_id_fkey");
            });

            modelBuilder.Entity<UsersTariff>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users_tariffs");

                entity.Property(e => e.AbonentId).HasColumnName("abonent_id");

                entity.Property(e => e.TariffId).HasColumnName("tariff_id");

                entity.HasOne(d => d.Abonent)
                    .WithMany()
                    .HasForeignKey(d => d.AbonentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_tariffs_abonent_id_fkey");

                entity.HasOne(d => d.Tariff)
                    .WithMany()
                    .HasForeignKey(d => d.TariffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_tariffs_tariff_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
