using dotKnowledge.Data;
using dotKnowledge.Endpoints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("Articles") ?? "Data Source=dotkb.db";

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlite<KBDbContext>(connString);

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

CategoryEndpoints.Endpoints(app);
ArticleEndpoints.Endpoints(app);

app.Run();
