using Kanban.API.Filters;
using Kanban.Application.Extensions;
using Kanban.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cors = "Cors";
builder.Services.AddInjectionApplication();
builder.Services.AddInjectionInfraestructure();

builder.Services.AddControllers(options =>
{
  options.Filters.Add(typeof(BadRequestParse));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddPolicy(
    name: cors,
    builder =>
    {
      builder.WithOrigins("*");
      builder.AllowAnyMethod();
      builder.AllowAnyHeader();
    });
});

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
