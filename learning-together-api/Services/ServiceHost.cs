namespace learning_together_api.Services
{
    using Data;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using pathways_common.Authentication.TokenAcquisition;
    using pathways_common.Interfaces.Services;
    using pathways_common.Services;

    public class ServiceHost : IServiceHost
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly DataContext dataContext;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ITokenAcquisition tokenAcquisition;

        public ServiceHost(DataContext dataContext, IHostingEnvironment hostingEnvironment, IOptions<AppSettings> appSettings, ITokenAcquisition tokenAcquisition)
        {
            this.dataContext = dataContext;
            this.hostingEnvironment = hostingEnvironment;
            this.appSettings = appSettings;
            this.tokenAcquisition = tokenAcquisition;
        }

        public IUserService GetUserService()
        {
            return new UserService(this.dataContext);
        }

        public ILocationService GetLocationService()
        {
            return new LocationService(this.dataContext);
        }

        public IRoleService GetRoleService()
        {
            return new RoleService(this.dataContext);
        }

        public IDisciplineService GetDisciplineService()
        {
            return new DisciplineService(this.dataContext);
        }

        public IWorkshopService GetWorkshopService()
        {
            return new WorkshopService(this.dataContext);
        }

        public ICategoryService GetCategoryService()
        {
            return new CategoryService(this.dataContext);
        }

        public IImageStorageService GetImageStorageService()
        {
            return new LocalStorageImageService(this.hostingEnvironment, this.appSettings);
        }

        public IWorkshopAttendeeService GetWorkshopAttendeeService()
        {
            return new WorkshopAttendeeService(this.dataContext);
        }

        public IMSGraphService GetMicrosoftGraphService()
        {
            return new MicrosoftGraphService(this.tokenAcquisition);
        }
    }
}