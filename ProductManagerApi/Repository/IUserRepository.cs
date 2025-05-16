using ProductManagerApi.Models;

namespace ProductManagerApi.Repository
{
    public interface IUserRepository
    {

        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task Add(UserModel user);
        Task Update(UserModel user);
        Task Delete(int id);
    }
}
