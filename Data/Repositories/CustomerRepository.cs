using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;

    public CustomerRepository(DataContext context) // Konstruktör som tar emot DataContext för att kunna hantera databasen.
    {
        _context = context ??  throw new ArgumentNullException(nameof(context));
    }

    public bool Create(CustomerEntity customerEntity) // Skapa en ny kund.
    {
        _context.Customers.Add(customerEntity);
        return _context.SaveChanges() > 0;
    }

    public IEnumerable<CustomerEntity> GetAll() // Hämta alla kunder inkl. deras projekt.
    {
        return _context.Customers.Include(c => c.Projects).ToList();
    }

    public CustomerEntity? GetById(int id)
    {
        return _context.Customers.Include(c => c.Projects).FirstOrDefault(c => c.Id == id);
    }


}
