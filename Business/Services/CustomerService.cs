using Data.Entities;
using Data.Interfaces;

namespace Data.Business.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public CustomerEntity CreateCustomer(string name)
    {
        var customer = new CustomerEntity { Name = name };
        _customerRepository.Create(customer);

        return _customerRepository.GetById(customer.Id) ?? customer;
    }

    public List<CustomerEntity> GetAllCustomers()
    {
        return _customerRepository.GetAll().ToList();
    }
}

