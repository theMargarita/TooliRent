using Domain.Core.Interfaces;
using Infrastructure.Models;

namespace Domain.Core.Core_Interfaces
{
    public interface IUnitOfWork
    {
        //this interface will encapsulate all repositories
        IToolRepository Tools { get; }
        ICustomerRepository Customers { get; }
        IRentalRepository Rentals { get; }
        IRepository<Category> Categories { get; }
        Task<int> SaveChangesAsync();
    }
}
