namespace learning_together_api.Services
{
    using Data;

    public interface IWorkshopService : IGetDataService<Workshop>
    {
        Workshop Create(Workshop workhop);
    }
}