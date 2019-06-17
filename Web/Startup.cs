using System;
using System.Text;
using Core.AuthService;
using Core.AuthService.Config;
using Core.ChatService;
using Core.SmsService;
using Core.UserService;
using Data.DbContexts;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Web.Controllers;
using AutoMapper;
using Core.Mapping;
using Core.KevenegarSmsService;
using Core.KevenegarSmsService.Configuration;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

                public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var KavenegarKey = Environment.GetEnvironmentVariable("OpenChat_Kavenegar");
            var JwtEncryption = Environment.GetEnvironmentVariable("OpenChat_JWT_Encryption");
            var MySqlConnectionString = Environment.GetEnvironmentVariable("OpenChat_MySqlConnectionString");
            // Configurations
            var authConfig = Configuration.GetSection("Auth").Get<AuthServiceConfig>();
            services.Configure<AuthServiceConfig>(Configuration.GetSection("Auth"));
            services.Configure<KavenegarConfiguration>(Configuration.GetSection("Kavenegar"));
            //Auth
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = true;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //DbContexts
            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(MySqlConnectionString, b => b.MigrationsAssembly("Web")));
            //Services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthService, AuthService>(p=>new AuthService(JwtEncryption,p.GetService<IUserService>(),p.GetService<IMapper>(),p.GetService<ISmsService>(),p.GetService<IUnitOfWork>()));
            services.AddTransient<ISmsService, KavenegarService>(p=>new KavenegarService(KavenegarKey));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IChatService, ChatService>();
            //Mapper
            var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //SignalR
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "Open Chat API Documnetation";
                    document.Info.Version = "v1.0";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Email = "ParsaGachkar@Gmail.com",
                        Name = "Parsa Gachkar"
                    };
                };
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());




            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/hubs/chat");
            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp/";
                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = TimeSpan.FromMinutes(5);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });


        }
    }
}

