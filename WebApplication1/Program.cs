using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Helpers;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


////add datacontext
string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

////repository
////builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

////cloudinary setting
builder.Services.AddScoped<IPhotoService,PhotoService>();


////authentication service

////secret key
var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;

////key
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key,
        };
    });

////AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapp));

////patch
builder.Services.AddControllers().AddNewtonsoftJson();  // but it is deprecated

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

//authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
