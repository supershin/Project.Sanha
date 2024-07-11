using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.Sanha.Web.Data
{
    public partial class TitleDbContext : DbContext
    {
        public TitleDbContext()
        {
        }

        public TitleDbContext(DbContextOptions<TitleDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sanha_tm_ProjectShopservice> Sanha_tm_ProjectShopservice { get; set; } = null!;
        public virtual DbSet<Sanha_tm_ResourceType> Sanha_tm_ResourceType { get; set; } = null!;
        public virtual DbSet<Sanha_tm_Shopservice> Sanha_tm_Shopservice { get; set; } = null!;
        public virtual DbSet<Sanha_tm_UnitQuota_Mapping> Sanha_tm_UnitQuota_Mapping { get; set; } = null!;
        public virtual DbSet<Sanha_tr_Shopservice_Resource> Sanha_tr_Shopservice_Resource { get; set; } = null!;
        public virtual DbSet<Sanha_tr_UnitShopservice> Sanha_tr_UnitShopservice { get; set; } = null!;
        public virtual DbSet<Sanha_ts_Shopservice_Trans> Sanha_ts_Shopservice_Trans { get; set; } = null!;
        public virtual DbSet<master_project> master_project { get; set; } = null!;
        public virtual DbSet<master_unit> master_unit { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=10.0.20.14;Initial Catalog=AfterSale;User ID=aftersale;Password=aftersale@2022;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_CI_AS");

            modelBuilder.Entity<Sanha_tm_ProjectShopservice>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Sanha_tm_ResourceType>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Sanha_tm_Shopservice>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Sanha_tm_UnitQuota_Mapping>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Sanha_tr_Shopservice_Resource>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Sanha_tr_UnitShopservice>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Sanha_ts_Shopservice_Trans>(entity =>
            {
                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FlagActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<master_project>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<master_unit>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
