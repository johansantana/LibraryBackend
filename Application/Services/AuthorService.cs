using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;

namespace Application.Services;

public class AuthorService : IAuthorService
{
    private readonly AuthorRepository _repository;

    public AuthorService(AuthorRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Author>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Author?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<Author?> CreateAsync(Author author) => _repository.CreateAsync(author);

    public Task<Author?> UpdateAsync(int id, Author author) => _repository.UpdateAsync(id, author);

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}