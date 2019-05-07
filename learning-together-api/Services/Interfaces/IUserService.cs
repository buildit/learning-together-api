namespace learning_together_api.Services
{
    using Data;

    public interface IUserService : IGetDataService<User>, ISearchService<User>
    {
        User Create(User user);

        void Update(int userId, User user);

        void Delete(int userId, int id);

        User GetByIdWithIncludes(int id);

        User Retrieve(string username);

        User RetrieveOrCreate(string authenticatedEmail, string userName);
    }
}