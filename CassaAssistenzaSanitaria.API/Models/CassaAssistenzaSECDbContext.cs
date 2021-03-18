using CassaAssistenzaSanitaria.API.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class CassaAssistenzaSECDbContext : IdentityDbContext<ApplicationUser>
    {
        public CassaAssistenzaSECDbContext(DbContextOptions<CassaAssistenzaSECDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
