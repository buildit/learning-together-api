namespace learning_together_api.Services
{
    using pathways_common.Interfaces.Services;

    public interface IServiceHost
    {
        IUserService GetUserService();

        ILocationService GetLocationService();

        IRoleService GetRoleService();

        IDisciplineService GetDisciplineService();

        IWorkshopService GetWorkshopService();

        ICategoryService GetCategoryService();

        IImageStorageService GetImageStorageService();

        IWorkshopAttendeeService GetWorkshopAttendeeService();

        IMSGraphService GetMicrosoftGraphService();
    }
}