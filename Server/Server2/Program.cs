using DataLayer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json.Serialization;
using Server.Services;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o=> o.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<DiceMatchDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseCors(o => o.AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
