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
    // Minimal fake handler to return preconfigured HttpResponseMessage
    internal class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(_response);
    }

    public class AuthorServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAuthors()
        {
            var expected = new List<Author> { new Author { Id = 1, FirstName = "A", LastName = "B" } };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expected)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new AuthorRepository(http);
            var service = new AuthorService(repo);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsAuthor_WhenFound()
        {
            var expected = new Author { Id = 2, FirstName = "X" };
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(expected)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new AuthorRepository(http);
            var service = new AuthorService(repo);

            var result = await service.GetByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(2, result!.Id);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedAuthor()
        {
            var toCreate = new Author { FirstName = "New" };
            var created = new Author { Id = 5, FirstName = "New" };

            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = JsonContent.Create(created)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new AuthorRepository(http);
            var service = new AuthorService(repo);

            var result = await service.CreateAsync(toCreate);

            Assert.NotNull(result);
            Assert.Equal(5, result!.Id);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedAuthor()
        {
            var toUpdate = new Author { FirstName = "Up" };
            var updated = new Author { Id = 3, FirstName = "Up" };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(updated)
            };

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new AuthorRepository(http);
            var service = new AuthorService(repo);

            var result = await service.UpdateAsync(3, toUpdate);

            Assert.NotNull(result);
            Assert.Equal(3, result!.Id);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenDeleted()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var http = new HttpClient(new FakeHttpMessageHandler(response));
            var repo = new AuthorRepository(http);
            var service = new AuthorService(repo);

            var result = await service.DeleteAsync(7);

            Assert.True(result);
        }
    }
}
