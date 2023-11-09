using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Resources.Constants;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Authentication;
using ASI.Basecode.WebApp.Extensions.Configuration;
using ASI.Basecode.WebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;

namespace ASI.Basecode.WebApp
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureDependencies(services);       // Configuration for dependency injections           
            this.ConfigureDatabase(services);           // Configuration for database connections
            this.ConfigureMapper(services);             // Configuration for entity model and view model mapping
            this.ConfigureCors(services);               // Configuration for CORS
            this.ConfigureAuth(services);               // Configuration for Authentication logic
            this.ConfigureMVC(services);                // Configuration for MVC                  


            // Add services to the container.
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //services.AddLogging(x => x.AddConfiguration(Configuration.GetLoggingSection()).AddConsole().AddDebug());
            PathManager.Setup(this.Configuration.GetSetupRootDirectoryPath());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");     // Enables site to redirect to page when an exception occurs
                app.UseHsts();                              // Enables the Strict-Transport-Security header.
            }

            this.ConfigureLogger(app, env);
            app.UseStaticFiles();           // Enables the use of static files
            app.UseHttpsRedirection();      // Enables redirection of HTTP to HTTPS requests.
            app.UseCors("CorsPolicy");      // Enables CORS                              
            app.UseRouting();
            app.UseAuthentication();        // Enables the ConfigureAuth service.
            app.UseMvc();
            app.UseAuthorization();

            this.ConfigureRoutes(app);      // Configuration for API controller routing
            this.ConfigureAuth(app);        // Configuration for Token Authentication
        }
    }
    /// <summary>
    /// For configuring services on application startup.
    /// </summary>
    /// <remarks>
    /// <para>Method call sequence for instances of this class:</para>
    /// <para>1. constructor</para>
    /// <para>2. <see cref="ConfigureServices(IServiceCollection)"/></para>
    /// <para>3. (create <see cref="IApplicationBuilder"/> instance)</para>
    /// <para>4. <see cref="ConfigureApp(IApplicationBuilder, IWebHostEnvironment)"/></para>
    /// </remarks>
    internal partial class StartupConfigurer
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        private IConfiguration Configuration { get; }

        private IApplicationBuilder _app;

        private IWebHostEnvironment _environment;

        private IServiceCollection _services;

        /// <summary>
        /// Initialize new <see cref="StartupConfigurer"/> instance using <paramref name="configuration"/>
        /// </summary>
        /// <param name="configuration"></param>
        public StartupConfigurer(IConfiguration configuration)
        {
            this.Configuration = configuration;

            PathManager.Setup(this.Configuration.GetSetupRootDirectoryPath());

            var token = this.Configuration.GetTokenAuthentication();
            this._signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.SecretKey));
            this._tokenProviderOptions = TokenProviderOptionsFactory.Create(token, this._signingKey);
            this._tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = this._signingKey,
                ValidateIssuer = true,
                ValidIssuer = Const.Issuer,
                ValidateAudience = true,
                ValidAudience = token.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            PasswordManager.SetUp(this.Configuration.GetSection("TokenAuthentication"));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            this._services = services;

            services.AddMemoryCache();

            // Register SQL database configuration context as services.
            services.AddDbContext<AsiBasecodeDBContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(120));
            });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddHttpContextAccessor();

            //Configuration
            services.Configure<TokenAuthentication>(Configuration.GetSection("TokenAuthentication"));
            
            // Session
            services.AddSession(options =>
            {
                options.Cookie.Name = Const.Issuer;
            });

            // DI Services AutoMapper(Add Profile)
            //this.ConfigureAutoMapper();

            // DI Services
            this.ConfigureOtherServices();

            // Authorization (Add Policy)
            this.ConfigureAuthorization();

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 1024 * 1024 * 100;
            });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IGenreService, GenreService>();
        }

        /// <summary>
        /// Configure application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this._app = app;
            this._environment = env;

            if (!this._environment.IsDevelopment())
            {
                this._app.UseHsts();
            }

            //this.ConfigureLogger();

            this._app.UseTokenProvider(_tokenProviderOptions);

            this._app.UseHttpsRedirection();
            this._app.UseStaticFiles();

            // Localization
            var options = this._app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            this._app.UseRequestLocalization(options.Value);

            this._app.UseSession();
            this._app.UseRouting();

            this._app.UseAuthentication();
            this._app.UseAuthorization();
        }
    }
}
