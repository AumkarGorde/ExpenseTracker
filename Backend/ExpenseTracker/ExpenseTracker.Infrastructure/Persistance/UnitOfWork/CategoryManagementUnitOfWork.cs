using ExpenseTracker.Application;
using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public class CategoryManagementUnitOfWork : ICategoryManagementUnitOfWork
    {
        private readonly ExpenseTrackerDBContext _dbContext;
        public ICategoriesRepository CategoriesRepository { get; }

        public IRepository<CategoryUser> CategoryUserRepository { get; }

        public IRepository<Users> UsersRepository { get; }


        public CategoryManagementUnitOfWork(ExpenseTrackerDBContext dbContext)
        {
            _dbContext = dbContext;
            CategoriesRepository = new CategoriesRepository(_dbContext);
            CategoryUserRepository = new Repository<CategoryUser>(_dbContext);
            UsersRepository = new Repository<Users>(_dbContext);
        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
