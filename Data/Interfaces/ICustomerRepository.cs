using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    bool Create(CustomerEntity customerEntity);
    IEnumerable<CustomerEntity> GetAll();
    CustomerEntity? GetById(int id);
}
