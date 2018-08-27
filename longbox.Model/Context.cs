using Microsoft.EntityFrameworkCore;

namespace longbox.Model
{
    public class LongboxContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Comic> Comics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }
}
