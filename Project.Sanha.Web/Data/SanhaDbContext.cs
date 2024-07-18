using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.Sanha.Web.Data
{
    public partial class SanhaDbContext : DbContext
    {
        public SanhaDbContext()
        {
        }

        public SanhaDbContext(DbContextOptions<SanhaDbContext> options)
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
        public virtual DbSet<master_relation> master_relation { get; set; } = null!;
        public virtual DbSet<master_unit> master_unit { get; set; } = null!;
        public virtual DbSet<user> users { get; set; } = null!;
        public virtual DbSet<user_projects> user_projects { get; set; } = null!;

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

            modelBuilder.Entity<master_relation>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<master_unit>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<user_projects>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
