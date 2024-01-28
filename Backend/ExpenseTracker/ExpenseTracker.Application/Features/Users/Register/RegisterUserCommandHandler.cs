using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUserService _userService;
        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool isExistingUser = await _userService.UserExistCheck(request.FirstName, request.LastName, request.Email);
            if (!isExistingUser) 
            {
                return await _userService.Register(request);
            }
            else
            {
                return false;
            }
        }
    }
}
