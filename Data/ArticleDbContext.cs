using dotKnowledge.Models;
using Microsoft.EntityFrameworkCore;

namespace dotKnowledge.Data
{
	public class ArticleDbContext : DbContext
	{
		public ArticleDbContext(DbContextOptions options) : base(options) { }
		public DbSet<Article> Articles { get; set; } = null!;
	}
}