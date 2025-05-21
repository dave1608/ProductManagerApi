using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Data;
using ProductManagerApi.Models;

namespace ProductManagerApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public UserModel Create(UserModel user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return user;
    }

    public IEnumerable<UserModel> GetAllUsers()
    {
        return _context.Users.Select(u => new UserModel
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role,
            IsActive = u.IsActive,
        });
    }

    public UserModel GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public UserModel GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public UserModel Update(UserModel user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser == null)
        {
            return null;
        }
        
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
        existingUser.Role = user.Role;
        existingUser.IsActive = user.IsActive;
        existingUser.UpdatedAt = DateTime.UtcNow;
        
        _context.Users.Update(existingUser);
        _context.SaveChanges();

        return existingUser;
    }

    public void Delete(UserModel user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}