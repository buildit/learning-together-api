namespace learning_together_api
{
    using System.IO;
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Logging;
    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DataContext>(c => c.UseNpgsql(this.Configuration.GetConnectionString("LearningTogether")))
                .BuildServiceProvider();
            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();

            IConfigurationSection appSettingsSection = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // this.SetupJwtAuth(services);
            this.SetupAzureAdAuth(services);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IWorkshopService, WorkshopService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageStorageService, LocalStorageImageService>();
            services.AddScoped<IWorkshopAttendeeService, WorkshopAttendeeService>();
        }

        private void SetupAzureAdAuth(IServiceCollection services)
        {
            services
                .AddAuthentication(sharedOptions => { sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(options =>
                {
                    options.Audience = this.Configuration["AzureAd:ClientId"];
                    options.Authority = $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}";
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> appSettings)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), appSettings.Value.ImageRootPath);

            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath), RequestPath = appSettings.Value.StaticServePath
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}