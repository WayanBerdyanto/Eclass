using System.Security.Claims;
using System.Text;
using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000") // Replace with your frontend's origin
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.Configure<EclassDatabaseSettings>(
    builder.Configuration.GetSection("EclassDatabase")
);

builder.Services.AddSingleton<StudentsServices>();
builder.Services.AddSingleton<UserServices>();
builder.Services.AddSingleton<LoginServices>();
builder.Services.AddControllers();

// AUTH
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Eclass API",
        Description = "Simple API documentation for OpenApi",
        Contact = new OpenApiContact
        {
            Name = "Wayan Berdyanto",
            Url = new Uri("https://www.linkedin.com/in/wayanberdyanto/")
        },
        License = new OpenApiLicense
        {
            Name = "Wayan Berdyanto",
            Url = new Uri("https://github.com/WayanBerdyanto/Eclass")
        }
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.MapControllers();

app.Run();

