namespace learning_together_api.Services
{
    using Data;

    public interface IUserService : IGetDataService<User>, ISearchService<User>
    {
        User Authenticate(string username, string password);

        User Create(User user, string password);

        void Update(int userId, User user, string password = null);

        void Delete(int userId, int id);

        User GetByIdWithIncludes(int id);
    }
}