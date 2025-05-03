using System;
using System.Collections.Generic;

namespace CustomerRepo;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
    Customer? GetById(Guid id);
    void Create(Customer customer);
    void Update(Customer customer);
    void Delete(Guid id);
}