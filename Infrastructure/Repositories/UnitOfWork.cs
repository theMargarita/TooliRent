using Domain.Core.Core_Interfaces;
using Domain.Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToolDbContext _context;
        private IToolRepository? _tools;
        private ICustomerRepository? _customers;
        private IRentalRepository? _rentals;
        private IRepository<Category>? _categories;

        public UnitOfWork(ToolDbContext context)
        {
            _context = context;
        }

        public IToolRepository Tools => _tools ??= new ToolRepository(_context);

        public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);

        public IRentalRepository Rentals => _rentals ??= new RentalRepository(_context);

        public IRepository<Category> Categories => _categories ??= new Repository<Category>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
