using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Models;

namespace WebAPI.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<dt_config> dt_config { get; set; }
        public virtual DbSet<dt_draw> dt_draw { get; set; }
        public virtual DbSet<dt_draw_participant> dt_draw_participant { get; set; }
        public virtual DbSet<dt_ads> dt_ads { get; set; }
        public virtual DbSet<dt_draw_prize> dt_draw_prize { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<user_role> user_role { get; set; }
        public virtual DbSet<role> role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=BookStoresDB");
            }
        }
    }
}
