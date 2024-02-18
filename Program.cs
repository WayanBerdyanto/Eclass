using Eclass.Models;
using Eclass.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EclassDatabaseSettings>(
    builder.Configuration.GetSection("EclassDatabase"));

builder.Services.AddSingleton<StudentsServices>();

builder.Services.AddSingleton<UserServices>();

builder.Services.AddSingleton<LoginServices>();

builder.Services.AddControllers();

// AUTH
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

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

app.MapGet("/", () => "Hello, World!");
app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. My secret")
    .RequireAuthorization();

app.MapControllers();

app.Run();
