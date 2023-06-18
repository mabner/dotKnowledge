using dotKnowledge.Models;
using Microsoft.EntityFrameworkCore;

namespace dotKnowledge.Data
{
    public class KBDbContext : DbContext
    {
        public KBDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}