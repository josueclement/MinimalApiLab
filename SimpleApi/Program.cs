using System;
using CustomerRepo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSingleton<ICustomerRepository, MemoryCustomerRepository>();

var app = builder.Build();


app.MapGet("/customers", (ICustomerRepository repository) =>
{
    return Results.Ok(repository.GetAll());
});

app.MapGet("/customers/{id}", (ICustomerRepository repository, Guid id) =>
{
    var customer = repository.GetById(id);
    return customer is not null ? Results.Ok(customer) : Results.NotFound();
});

app.MapPost("/customers", (ICustomerRepository repository, Customer customer) =>
{
    repository.Create(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("/customers/{id}", (ICustomerRepository repository, Guid id, Customer updatedCustomer) =>
{
    var customer = repository.GetById(id);
    if (customer is null)
    {
        return Results.NotFound();
    }
    repository.Update(updatedCustomer);
    return Results.Ok(updatedCustomer);
});

app.MapDelete("/customers/{id}", (ICustomerRepository repository, Guid id) =>
{
    repository.Delete(id);
    return Results.Ok();
});

app.Run();
