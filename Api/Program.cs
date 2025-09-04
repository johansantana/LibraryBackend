using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias
builder.Services.AddHttpClient<BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddHttpClient<AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
        options.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
