using Microsoft.EntityFrameworkCore;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class CassaAssistenzaADMDbContext : DbContext
    {
        string _connectionString;
        public CassaAssistenzaADMDbContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }

        public DbSet<Iscritto> Iscritti { get; set; }
        public DbSet<Prestazione> Prestazioni { get; set; }
	    public DbSet<Richiesta> Richieste { get; set; }
    }
}
