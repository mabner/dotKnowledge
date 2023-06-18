using dotKnowledge.Models;
using Microsoft.EntityFrameworkCore;

namespace dotKnowledge.Endpoints
{
    public class CategoryEndpoints
    {
        public static void Endpoints(WebApplication app)
        {
            app.MapGet("/category", async (Data.KBDbContext db) => await db.Categories.ToListAsync<Category>());

            app.MapPost("/category", async (Data.KBDbContext db, Category category) =>
            {
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
                return Results.Created($"/category/{category.Id}", category);
            });

            app.MapGet("/category/{id}", async (Data.KBDbContext db, int id) => await db.Categories.FindAsync(id));

            app.MapPut("/category/{id}", async (Data.KBDbContext db, Category updatecategory, int id) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null) return Results.NotFound();
                category.ParentId = updatecategory.ParentId;
                category.Name = updatecategory.Name;
                category.Content = updatecategory.Content;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/category/{id}", async (Data.KBDbContext db, int id) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null)
                {
                    return Results.NotFound();
                }

                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
