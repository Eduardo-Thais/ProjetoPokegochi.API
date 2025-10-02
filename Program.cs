using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoPokegochi.Authorization;
using ProjetoPokegochi.Data;
using ProjetoPokegochi.Model;
using ProjetoPokegochi.Services;
using System.Text;

namespace ProjetoPokegochi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var key = builder.Configuration["SymmetricSecurityKey"] ?? throw new InvalidOperationException("SymmetricSecurityKey not found in configuration");
            var db = builder.Configuration["ConnectionStrings:UsuarioDbContext"] ?? throw new InvalidOperationException("UsuarioDbContext not found in configuration");
            
            //Banco de dados
            builder.Services.AddDbContext<UsuarioDbContext>(options =>
            options.UseSqlServer(db));

            //Identity
            builder.Services.AddIdentity<Usuario, IdentityRole>()
            .AddEntityFrameworkStores<UsuarioDbContext>()
            .AddDefaultTokenProviders();
            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Authorization
            builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

            //Dependecy Injection
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<TokenService>();

            //Regras de autorização
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("IdadeMinima", policy =>
                    policy.AddRequirements(new IdadeMinima(18)));
            });

            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            //Swagger Adicionando o token JWT
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoPokegochi API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Digite: Bearer {seu token JWT}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            //Autenticação JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new
                    Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
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
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
