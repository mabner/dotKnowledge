using dotKnowledge.Data;
using dotKnowledge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("Articles") ?? "Data Source=dotkb.db";

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlite<ArticleDbContext>(connString);

builder.Services.AddSwaggerGen(s =>
{
	s.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = ".Knowledge",
		Description = "Preserving your knowledge for future reference.",
		Version = "v1"
	});
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Knowledge API V1"); });

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/article", async (ArticleDbContext db) => await db.Articles.ToListAsync());

app.MapPost("/article", async (ArticleDbContext db, Article article) =>
{
	await db.Articles.AddAsync(article);
	await db.SaveChangesAsync();
	return Results.Created($"/article/{article.Id}", article);
});

app.MapGet("/article/{id}", async (ArticleDbContext db, int id) => await db.Articles.FindAsync(id));

app.MapPut("/article/{id}", async (ArticleDbContext db, Article updatearticle, int id) =>
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

app.MapDelete("/article/{id}", async (ArticleDbContext db, int id) =>
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

app.Run();
