using DemoAPI.DataContext;
using DemoAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IRequestObjService, RequestObjService>();
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<MSSQLContext> (options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddSingleton<MyMemoryCache>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
