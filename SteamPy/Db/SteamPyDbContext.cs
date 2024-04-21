using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using steamPy.Models;
using SteamPy.SDK.Models.Domain;

namespace steamPy.Db
{
    public class SteamPyDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GamePriceInfoHistory>()
            //    .HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
        public SteamPyDbContext(DbContextOptions<SteamPyDbContext> options): base(options)
        {

            
        }

        /// <summary>
        /// 游戏价格存档
        /// </summary>
        public DbSet<GamePriceInfoHistory> GamePriceInfoHistories { get; set; }

        public DbSet<GamePriceInfo> GamePriceInfos { get; set; }

        /// <summary>
        /// 游戏价格存档
        /// </summary>
        public DbSet<UserWatch> UserWatchList { get; set; }

        public DbSet<UserInfo> Users { get; set; }  
    }
}
