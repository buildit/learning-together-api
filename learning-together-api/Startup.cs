namespace learning_together_api
{
    using System.IO;
    using AutoMapper;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;
    using pathways_common.Core;
    using Services;

    public class Startup : PathwaysStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override void AddEntityFramework(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<DataContext>(c => c.UseNpgsql(this.Configuration.GetConnectionString("LearningTogether")))
                .BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigurePathwaysServices(services);

            IConfigurationSection appSettingsSection = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddAutoMapper();

            services.AddScoped<IServiceHost, ServiceHost>();
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> appSettings)
        {
            this.ConfigurePathways(app, env);

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), appSettings.Value.ImageRootPath);

            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath), RequestPath = appSettings.Value.StaticServePath
            });
        }
    }
}