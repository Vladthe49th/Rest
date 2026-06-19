using ItemsApi.Models;
using ItemsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentSql> Students { get; set; }
    }
}