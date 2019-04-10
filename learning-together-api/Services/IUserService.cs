namespace learning_together_api.Services
{
    using Data;

    public interface IUserService : IGetDataService<User>
    {
        User Authenticate(string username, string password);

        User Create(User user, string password);

        void Update(User user, string password = null);

        void Delete(int id);

        User GetByIdWithIncludes(int id);
    }
}