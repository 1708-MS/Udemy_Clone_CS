using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Udemy_WebApp.Application.DTO.MappingProfile;
using Udemy_WebApp.Application.Interfaces.IRepository;
using Udemy_WebApp.Application.Interfaces.RoleServiceContract;
using Udemy_WebApp.Application.Interfaces.UserServiceContracts;
using Udemy_WebApp.Domain.Models;
using Udemy_WebApp.Infrastructure;
using Udemy_WebApp.Infrastructure.Configurations;
using Udemy_WebApp.Infrastructure.DataAccess;
using Udemy_WebApp.Infrastructure.InterfaceImplementations.Repository;
using Udemy_WebApp.Infrastructure.InterfaceImplementations.RoleService;
using Udemy_WebApp.Infrastructure.InterfaceImplementations.UserServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"), b => b.MigrationsAssembly("Udemy_WebApp.Infrastructure"));
});

// Adding Identity as a Service
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(DtoMappingProfile));

// Register IUserService and its implementation UserService
builder.Services.AddTransient<IUserService, UserService>();

// Register IRoleInitializeService and its implementation RoleInitializeService
builder.Services.AddTransient<IRoleInitializeService, RoleInitializeService>();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Udemy_WebApp.WebApi", Version = "v1" });
//});

//JWT Authentication
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSetting = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSetting.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
