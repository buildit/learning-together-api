namespace learning_together_api
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
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
    using Microsoft.IdentityModel.Tokens;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper();

            this.SetupJwtAuth(services);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IWorkshopService, WorkshopService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, LocalStorageImageService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> appSettings)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", appSettings.Value.ImageRootPath);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath), RequestPath = appSettings.Value.StaticServePath
            });

            app.UseAuthentication();
            app.UseMvc();
        }

        private void SetupJwtAuth(IServiceCollection services)
        {
            IConfigurationSection appSettingsSection = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            IUserService userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            int userId = int.Parse(context.Principal.Identity.Name);
                            User user = userService.Retrieve(userId);
                            if (user == null)
                            {
                                // return unauthorized if user no longer exists
                                context.Fail("Unauthorized");
                            }

                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}