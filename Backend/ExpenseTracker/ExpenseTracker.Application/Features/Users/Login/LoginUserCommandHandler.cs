using ExpenseTracker.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public LoginUserCommandHandler(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;

        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            if (await _userService.ValidateUser(request.UserName, request.Password))
            {
                var user = await _userService.GetUserByUserName(request.UserName);
                string token = _authenticationService.GenerateJwtToken(request.UserName, "USER"); //TODO : Dynamic role binding
                return new LoginUserCommandResponse()
                {
                    Token = token,
                    FirstName = user.FirstName,
                    UserName = user.UserName,
                    UserId = user.UserId.ToString(),
                };
            }
            else
            {
                return new LoginUserCommandResponse()
                {
                    Token = string.Empty,
                    FirstName = string.Empty,
                    UserName = string.Empty,
                    UserId = string.Empty,
                };
            }

        }
    }
}
