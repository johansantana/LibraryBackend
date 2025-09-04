using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain;
using Infrastructure.Repositories;
using Xunit;

namespace Application.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsBooks()
        {
            var expected = new List<Book> { new Book { Id = 1, Title = "T1" } };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expected)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new BookRepository(http);
            var service = new BookService(repo);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsBook_WhenFound()
        {
            var expected = new Book { Id = 9, Title = "Found" };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expected)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new BookRepository(http);
            var service = new BookService(repo);

            var result = await service.GetByIdAsync(9);

            Assert.NotNull(result);
            Assert.Equal(9, result!.Id);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedBook()
        {
            var toCreate = new Book { Title = "New" };
            var created = new Book { Id = 11, Title = "New" };

            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = JsonContent.Create(created)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new BookRepository(http);
            var service = new BookService(repo);

            var result = await service.CreateAsync(toCreate);

            Assert.NotNull(result);
            Assert.Equal(11, result!.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedBook()
        {
            var toUpdate = new Book { Title = "Up" };
            var updated = new Book { Id = 4, Title = "Up" };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(updated)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new BookRepository(http);
            var service = new BookService(repo);

            var result = await service.UpdateAsync(4, toUpdate);

            Assert.NotNull(result);
            Assert.Equal(4, result!.Id);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenDeleted()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new BookRepository(http);
            var service = new BookService(repo);

            var result = await service.DeleteAsync(2);

            Assert.True(result);
        }
    }
}
