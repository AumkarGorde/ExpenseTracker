using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface ICategoryManagementUnitOfWork
    {
        ICategoriesRepository CategoriesRepository { get; }
        IRepository<CategoryUser> CategoryUserRepository { get; }
        IRepository<Users> UsersRepository { get; }
        Task SaveChanges();
    }
}
