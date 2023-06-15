using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace dotKnowledge.Data
{
	public class CategoryDbContext : DbContext
	{
		public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories => Set<Category>();
	}
}