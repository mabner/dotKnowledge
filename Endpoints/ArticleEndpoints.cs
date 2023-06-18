using dotKnowledge.Data;
using dotKnowledge.Models;
using Microsoft.EntityFrameworkCore;

namespace dotKnowledge.Endpoints
{
	public class ArticleEndpoints
	{
		public static void Endpoints(WebApplication app)
		{
			app.MapGet("/article", async (Data.KBDbContext db) => await db.Articles.ToListAsync<Article>());

			app.MapPost("/article", async (Data.KBDbContext db, Article article) =>
			{
				await db.Articles.AddAsync(article);
				await db.SaveChangesAsync();
				return Results.Created($"/article/{article.Id}", article);
			});

			app.MapGet("/article/{id}", async (Data.KBDbContext db, int id) => await db.Articles.FindAsync(id));

			app.MapPut("/article/{id}", async (Data.KBDbContext db, Article updatearticle, int id) =>
			{
				var article = await db.Articles.FindAsync(id);
				if (article is null) return Results.NotFound();
				article.CategoryId = updatearticle.CategoryId;
				article.UpdatedAt = DateTime.Now;
				article.Title = updatearticle.Title;
				article.Content = updatearticle.Content;
				article.IsActive = updatearticle.IsActive;
				await db.SaveChangesAsync();
				return Results.NoContent();
			});

			app.MapDelete("/article/{id}", async (Data.KBDbContext db, int id) =>
			{
				var article = await db.Articles.FindAsync(id);
				if (article is null)
				{
					return Results.NotFound();
				}

				db.Articles.Remove(article);
				await db.SaveChangesAsync();
				return Results.Ok();
			});
		}
	}
}