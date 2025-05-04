using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using CustomerRepo;

var baseAddress = new Uri("http://localhost:5001");

var client = new HttpClient();

for (var i = 0; i < 10; i++)
{
    var c = new Customer(Guid.NewGuid(), $"Customer {i + 1}");
    var serializedCustomer = JsonSerializer.Serialize(c);
    var str = new StringContent(serializedCustomer, System.Text.Encoding.UTF8, "application/json");
    var response = await client.PostAsync(baseAddress + "customers", str);
    Console.WriteLine(response.IsSuccessStatusCode ? "Success" : "Failed");
}

var customersResponse = await client.GetAsync(baseAddress + "customers");

if (customersResponse.IsSuccessStatusCode)
{
    var customers = await customersResponse.Content.ReadFromJsonAsync<List<Customer>>();
    Console.WriteLine(customers);
}
else
{
    Console.WriteLine("Failed");
}
