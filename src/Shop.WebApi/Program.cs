using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using ShoesShop.Application;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Entities;
using ShoesShop.Persistence;
using ShoesShop.WebApi.Dto.Mapping;
using ShoesShop.WebApi.Middleware;
using ShoesShop.WebApi.Services.Authentication;
using ShoesShop.WebApi.Services.CurrentUserService;
using ShoesShop.WebApi.Services.TokenService;

namespace ShoesShop.WebAPI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app, builder.Environment);
            InitializeDb(app);
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddControllers()
                            .AddJsonOptions(x =>
                            {
                                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                            });
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new DtoProfiles());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddRouting();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Version = "1.04",
                        Title = "Shoes Shop API",
                        Contact = new OpenApiContact()
                        {
                            Name = "Shape Cat",
                            Url = new Uri("https://github.com/ShapeTheMoonlight")
                        },
                    });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                var xmlPath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlPath));
            });
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.UpdateRoles, policy => policy.RequireRole(Roles.Administrator.ToString()));
                options.AddPolicy(Policies.UpdateGoods, policy => policy.RequireRole(Roles.Manager.ToString(), Roles.Administrator.ToString()));

            });
            builder.Services.AddSingleton<ITokenService>(new TokenService(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"]));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddHttpContextAccessor();
            ConfigureLogger(builder);
        }

        private static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public static void InitializeDb(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<ShopDbContext>();
                    DbInitializer.Initialize(context);
                    context.AddRole(Roles.Administrator, "admin", "qwerty44");
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while app startup");
                }
            }
        }

        public static void ConfigureLogger(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                          .WriteTo.File(Path.Combine("Logs", "apiLog-.txt"), rollingInterval: RollingInterval.Day)
                          .CreateLogger();
            builder.Host.UseSerilog();
        }
    }
}
