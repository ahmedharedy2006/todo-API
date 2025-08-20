using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using todo_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.Title = "Scalar API Reference";
        o.Theme = ScalarTheme.BluePlanet;
        o.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        o.CustomCss = "";
        o.ShowSidebar = true;
    });
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ✅ Apply migrations at startup
DbInit.Migrate(app);

app.Run();
