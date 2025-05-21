using ProductManagerApi.Enums;

namespace ProductManagerApi.Dtos;

public class RegisterUserDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public UserEnum Role { get; set; }
    public bool IsActive { get; set; }
}