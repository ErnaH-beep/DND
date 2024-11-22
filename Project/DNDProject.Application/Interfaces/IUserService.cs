using DNDProject.Domain.Models;

namespace DNDProject.Application.Interfaces;

public interface IUserService
{
    public List<User> GetUsers();
    public Task<User> SaveUserAsync(User user);
}
