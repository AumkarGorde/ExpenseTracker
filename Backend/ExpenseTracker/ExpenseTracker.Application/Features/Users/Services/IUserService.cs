using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface IUserService
    {
        Task<bool> Register(RegisterUserCommand request);
        Task<bool> UserExistCheck(string firstName, string LastName, string email);
        Task<bool> ValidateUser(string userName, string password);
        Task<Users> GetUserByUserName(string userName);
        Task<GetUserDetailsResponse> GetUserDetails(GetUserDetailsRequest userName);
        Task<UpdateUserDetailsResponse> UpdateUser(UpdateUserDetailsRequest userName);
    }
}
