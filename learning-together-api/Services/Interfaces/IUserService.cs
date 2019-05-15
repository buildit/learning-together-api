namespace learning_together_api.Services
{
    using Data;
    using pathways_common.Interfaces;

    public interface IUserService : IADUserService<User>, ISearchService<User>
    {
        void Update(int userId, User userParam);

        void Delete(int userId, int id);
    }
}