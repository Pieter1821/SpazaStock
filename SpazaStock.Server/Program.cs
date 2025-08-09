
using Microsoft.EntityFrameworkCore.SqlServer;
using SpazaStock.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SpazaStock.Server.Models.SpazaStockDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SpazaStockDb")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("ReactFrontend");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
