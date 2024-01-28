using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseTracker.Domain;

namespace ExpenseTracker.Application
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBudgetRepository _budgetRepository;
        private const string CLASS_NAME = "UserService";
        public UserService(ICustomLogger logger, IMapper mapper, IUserRepository userRepository, IBudgetRepository budgetRepository) 
            : base(logger, mapper)
        {
            _userRepository = userRepository;
            _budgetRepository = budgetRepository;

        }

        public async Task<bool> Register(RegisterUserCommand request)
        {
            try
            {
                _logger.LogInfo(CLASS_NAME, "Register", "Initiate Registration Process");
                Users users = new Users()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = CreateUserName(request.FirstName, request.LastName),
                    Email = request.Email,
                    Password = request.Password,
                };
                await _userRepository.AddAsync(users);
                await _userRepository.SaveChangesAsync();

                var budget = new Budget()
                {
                    BudgetLimit = request.BudgetLimit,
                    UserId = users.UserId,
                    SavingsGoal = request.SavingsGoal,
                    Balance = request.Balance
                };
                await _budgetRepository.AddAsync(budget);
                await _userRepository.SaveChangesAsync();

                _logger.LogInfo(CLASS_NAME, "Register", "Registration Process Completed");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(CLASS_NAME, "Register", $"Error in Registration Process : {ex.Message}", ex);
                return false;
            }

        }

        public async Task<bool> UserExistCheck(string firstName, string LastName, string email)
        {
            try
            {
                return await _userRepository.UserExistCheck(firstName, LastName, email);
            }
            catch (Exception ex)
            {
                _logger.LogError(CLASS_NAME, "UserExistCheck", $"Error in checking User : {ex.Message}", ex);
                return false;
            }

        }

        private string CreateUserName(string firstName, string lastName)
        {
            string userName = $"{char.ToLower(firstName[0])}{lastName.ToLower()}";
            var existingUserName = _userRepository.IsUserNameExist(userName);
            if (!existingUserName.Contains(userName))
            {
                return userName;
            }
            int counter = 1;
            while (existingUserName.Contains($"{userName}{counter}"))
            {
                counter++;
            }
            return $"{userName}{counter}";
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            try
            {
                return await _userRepository.ValidateUser(userName, password);
            }
            catch (Exception ex)
            {
                _logger.LogError(CLASS_NAME, "ValidateUser", $"Error in validating User : {ex.Message}", ex);
                return false;
            }
        }

        public async Task<Users> GetUserByUserName(string userName)
        {
            try
            {
                return await _userRepository.GetUserByUserName(userName);
            }
            catch (Exception ex)
            {
                _logger.LogError(CLASS_NAME, "GetUserByUserName", $"Error getting User : {ex.Message}", ex);
                return new Users();
            }
        }

        public async Task<GetUserDetailsResponse> GetUserDetails(GetUserDetailsRequest request)
        {
            try
            {
                var user = await _userRepository.GetUsersDataById(request.UserId);
                if (user is not null)
                    return new GetUserDetailsResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BudgetId = user.Budget.BudgetId,
                        BudgetLimit = user.Budget.BudgetLimit,
                        Email = user.Email,
                        Balance = user.Budget.Balance,
                        SavingsGoal = user.Budget.SavingsGoal
                    };
                else
                    return new GetUserDetailsResponse();
            }
            catch (Exception)
            {
                return new GetUserDetailsResponse();
            }
        }

        public async Task<UpdateUserDetailsResponse> UpdateUser(UpdateUserDetailsRequest request)
        {
            try
            {
               var user = await _userRepository.GetByIdAsync(request.UserId);
               var budget = await _budgetRepository.GetByIdAsync(request.BudgetId);
                if (user is not null && budget is not null)
                {
                    _mapper.Map(request, user);
                    _userRepository.Update(user);
                    await _userRepository.SaveChangesAsync();

                    _mapper.Map(request, budget);
                    _budgetRepository.Update(budget);
                    await _budgetRepository.SaveChangesAsync();
                    return new UpdateUserDetailsResponse()
                    {
                        IsUpdated = true
                    };
                }
                return new UpdateUserDetailsResponse()
                {
                    IsUpdated = false
                };

            }
            catch (Exception)
            {
                return new UpdateUserDetailsResponse()
                {
                    IsUpdated = false
                };
            }
        }
    }
}
