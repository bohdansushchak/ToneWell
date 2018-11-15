using Microsoft.EntityFrameworkCore;
using DryIoc;
using ToneWell.Services;
using ToneWell.Database.Tables;

namespace ToneWell.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TrackDb> Tracks { get; set; }

        public DatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = App.MyContainer.Resolve<IDbPath>().GetDatabasePath("ToneWellDb.db");

            optionsBuilder.UseSqlite($"Filename={dbPath}");

        }
    }
}
