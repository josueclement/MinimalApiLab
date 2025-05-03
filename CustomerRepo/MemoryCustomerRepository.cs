using System;
using System.Collections.Generic;

namespace CustomerRepo;

public class MemoryCustomerRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, Customer> _customers = new();
    
    public IEnumerable<Customer> GetAll()
        => _customers.Values;

    public Customer? GetById(Guid id)
        => _customers.GetValueOrDefault(id);

    public void Create(Customer customer)
        => _customers.Add(customer.Id, customer);

    public void Update(Customer customer)
        => _customers[customer.Id] = customer;

    public void Delete(Guid id)
        => _customers.Remove(id);
}