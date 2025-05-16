using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Data;
using ProductManagerApi.Models;
using ProductManagerApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserModel> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task Add(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(UserModel user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existingUser != null)
        {
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }
    }


    public async Task Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
