using DNDProject.Domain.Models;

namespace DNDProject.FileStorage;

public record DataContainer
{
    public List<User> Users { get; set; } = [];
    public List<Todo> Todos { get; set; } = [];
}
