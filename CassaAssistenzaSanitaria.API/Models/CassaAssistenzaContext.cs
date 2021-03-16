using Microsoft.EntityFrameworkCore;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class CassaAssistenzaContext : DbContext
    {
        string _connectionString;
        public CassaAssistenzaContext(string connectionString)
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
