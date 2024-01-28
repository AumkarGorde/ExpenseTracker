using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    /// <summary>
    /// For domin specific queries
    /// </summary>
    public interface IUserRepository : IRepository<Users>
    {
        Task<bool> UserExistCheck(string firstName, string LastName, string email);
        List<string> IsUserNameExist(string userName);
        Task<bool> ValidateUser(string userName, string password);
        Task<int> SaveChangesAsync();
        IEnumerable<Users> GetUsersByIds(IEnumerable<Guid> userIds);
        Task<Users> GetUserByUserName(string userName);
        Task<Users> GetUsersDataById(Guid userId); // should be moved to a UOW
    }
}
