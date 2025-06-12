
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerWebAPI.Data;
using ServerWebAPI.Model;
using System.Text.Json.Serialization;
using System.Text;

namespace ServerWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					In = ParameterLocation.Header,
					Description = "Enter Token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});

			builder.Services
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
				{
					options.Password.RequireDigit = true;
					options.Password.RequiredLength = 8;
					options.Password.RequireNonAlphanumeric = true;
					options.Password.RequireUppercase = true;
					options.Password.RequireLowercase = true;
				})
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			var jwtSettings = builder.Configuration.GetSection("JwtSettings");

			builder.Services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = jwtSettings["Issuer"],
						ValidateAudience = true,
						ValidAudience = jwtSettings["Audience"],
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
					};
				});

			builder.Services.AddAuthorization();


			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(policy =>
				{
					policy.WithOrigins("https://localhost:7209");
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowCredentials();
				});
			});

			builder.Services.AddSignalR();

			var app = builder.Build();


			app.UseCors();
			app.MapHub<Hubs.XatHub>("/xatHub");


			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
    }
}
