namespace learning_together_api.Services
{
    using Data;
    using pathways_common.Interfaces;

    public interface IUserService : IGetDataService<User>, ISearchService<User>, IResolveService<User>
    {
        User Create(User user);

        void Update(int userId, User user);

        void Delete(int userId, int id);

        User GetByIdWithIncludes(int id);

        User RetrieveOrCreate(string authenticatedEmail, string userName);
    }
}