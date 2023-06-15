using dotKnowledge.Data;
using Microsoft.EntityFrameworkCore;

namespace dotKnowledge.Endpoints
{
	public static class CategoryEndpoints
	{
		public static void MapCategoryEndpoints(this WebApplication app)
		{
			app.MapGet("/categories", async (CategoryDbContext db) =>
			await db.Categories.ToListAsync()).WithName("GetCategories");

			app.MapGet("/categories/{id}", async (int id, CategoryDbContext db) =>
						await db.Categories.FindAsync(id)
								is Category category ? Results.Ok(category) : Results.NotFound()
						).WithName("GetCategoryById").Produces<Category>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);
		}
	}
}