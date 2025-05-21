using ProductManagerApi.Models;

namespace ProductManagerApi.Repository;

public interface IUserRepository
{
    UserModel Create(UserModel user);
    IEnumerable<UserModel> GetAllUsers();
    UserModel GetByEmail(string email);
    UserModel GetById(int id);
    UserModel Update(UserModel user);
    void Delete(UserModel user);
    
}