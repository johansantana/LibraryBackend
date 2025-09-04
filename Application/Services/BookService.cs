using Application.Interfaces;
using Domain;
using Infrastructure.Repositories;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly BookRepository _repository;

    public BookService(BookRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Book>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Book?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task<Book?> CreateAsync(Book book) => _repository.CreateAsync(book);

    public Task<Book?> UpdateAsync(int id, Book book) => _repository.UpdateAsync(id, book);

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}
