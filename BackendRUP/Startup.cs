using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WatchTogetherAPI.Auth;
using WatchTogetherAPI.Data;
using WatchTogetherAPI.Models;
using WatchTogetherAPI.Models.JWT;
using WatchTogetherAPI.Models.Mappings;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchTogetherAPI.Services.MovieService;
using AutoMapper;
using WatchTogetherAPI.Services.MicrosoftAccountService;
using WatchTogetherAPI.Services.LobbyService;
using WatchTogetherAPI.Services.NotificationService;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace WatchTogetherAPI
{
    public class Startup
    {

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMadwj1PVkH"; 
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CORS", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddHttpClient();
            services.AddControllers();
            services.AddDbContext<WatchTogetherContext>(options => options.UseMySql(Configuration.GetConnectionString("WatchTogetherContext"), new MySqlServerVersion(new Version(10, 1, 40)), mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddAutoMapper(typeof(ViewModelToEntityMappingProfile));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<WatchTogetherContext>()
                .AddDefaultTokenProviders();
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = false,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim("rol", "api_access"));
            });


            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMicrosoftAccountService, MicrosoftAccountService >();
            services.AddScoped<ILobbyService, LobbyService>();
            services.AddScoped<INotificationService, NotificationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORS");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
