using Domain;

namespace Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task<Author> CreateAsync(Author author);
    Task<Author> UpdateAsync(int id, Author author);
    Task<bool> DeleteAsync(int id);
}
