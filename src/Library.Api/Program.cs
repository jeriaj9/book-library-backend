using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var fakeApiUrl = new Uri(builder.Configuration["FakeRestApi:BaseUrl"]!);
var frontendUrl = builder.Configuration["FrontendUrl"]!;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(frontendUrl).AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddHttpClient<IBookRepository, BookRepository>(client => client.BaseAddress = fakeApiUrl);
builder.Services.AddHttpClient<IAuthorRepository, AuthorRepository>(client => client.BaseAddress = fakeApiUrl);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontend");

app.MapControllers();

app.Run();
