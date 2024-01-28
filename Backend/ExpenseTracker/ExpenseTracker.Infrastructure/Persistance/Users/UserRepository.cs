using ExpenseTracker.Application;
using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(ExpenseTrackerDBContext dBContext) : base(dBContext)
        {

        }

        public List<string> IsUserNameExist(string userName)
        {
            return _entities.Where(u => u.UserName == userName || u.UserName.StartsWith(userName + " "))
                .Select(u => u.UserName)
                .ToList();
        }

        public async Task<bool> UserExistCheck(string firstName, string LastName, string email)
        {
           return await _entities.AnyAsync(
                                    u=> u.FirstName == firstName 
                                    && u.LastName == LastName 
                                    && u.Email == email);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            return await _entities.AnyAsync(u=>u.UserName == userName && u.Password == password);
        }

        public IEnumerable<Users> GetUsersByIds(IEnumerable<Guid> userIds)
        {
            return _entities.Where(u=> userIds.Contains(u.UserId)).ToList();
        }

        public async Task<Users> GetUserByUserName(string userName)
        {
            return await _entities.Where(u=>u.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<Users> GetUsersDataById(Guid userId)
        {
            var response = await _entities.Where(u => u.UserId == userId)
                .Join(_dBContext.Budget, user => user.UserId, budget => budget.UserId,
                (user, budget) => new Users()
                {
                   FirstName = user.FirstName,
                   LastName = user.LastName,
                   Email = user.Email,
                   Budget = budget
                }).FirstOrDefaultAsync();
            return response;
        }
    }
}
