using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Services.Data.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
        {
        }

        public ModelDBContext(DbContextOptions<ModelDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<MasterDatum> MasterData { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SelectedItem> SelectedItems { get; set; } = null!;
        public virtual DbSet<SelectedNumber> SelectedNumbers { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Modelcon");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Arabic_100_BIN");

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.Action).HasMaxLength(50);

                entity.Property(e => e.ControllerName).HasMaxLength(50);

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemInUse)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemName).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterDatum>(entity =>
            {
                entity.HasKey(e => e.MasterDataId)
                    .HasName("PK_TbData");

                entity.Property(e => e.InUse)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MasterDataName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleIsactive).HasColumnName("RoleISActive");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SelectedItem>(entity =>
            {
                entity.HasKey(e => e.SelectedItemsId)
                    .HasName("PK_TbDataItem");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SelectedItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbDataItem_Items");

                entity.HasOne(d => d.MasterData)
                    .WithMany(p => p.SelectedItems)
                    .HasForeignKey(d => d.MasterDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbDataItem_TbMasterData");
            });

            modelBuilder.Entity<SelectedNumber>(entity =>
            {
                entity.ToTable("SelectedNumber");

                entity.HasOne(d => d.MasterData)
                    .WithMany(p => p.SelectedNumbers)
                    .HasForeignKey(d => d.MasterDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SelectedNumber_MasterData");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UserInfo");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CrDate).HasColumnType("datetime");

                entity.Property(e => e.Isactive).HasColumnName("ISActive");

                entity.Property(e => e.PassWord).HasMaxLength(50);

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Up_Date");

                entity.Property(e => e.UpUserId).HasColumnName("UpUserID");

                entity.Property(e => e.UserFullName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.CrDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Up_Date");

                entity.Property(e => e.UpuserId).HasColumnName("UPUserId");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_UserInfo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
