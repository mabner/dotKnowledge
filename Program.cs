using dotKnowledge.Data;
using dotKnowledge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ArticleDbContext>(options => options.UseInMemoryDatabase("items"));


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
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Knowledge API V1");
});

app.MapGet("/articles", async (ArticleDbContext db) => await db.Articles.ToListAsync());
app.MapPost("/article", async (ArticleDbContext db, Article article) =>
{
	await db.Articles.AddAsync(article);
	await db.SaveChangesAsync();
	return Results.Created($"/article/{article.Id}", article);
});
app.MapGet("/article/{id}", async (ArticleDbContext db, int id) => await db.Articles.FindAsync(id));


app.Run();
