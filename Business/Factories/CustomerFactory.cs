using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity CreateCustomer(string name)
    {
        return new CustomerEntity
        {
            Name = name,
        };
    }
        
}
