using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class UpdateUserDetailsHandler : IRequestHandler<UpdateUserDetailsRequest, UpdateUserDetailsResponse>
    {
        private readonly IUserService _userService;
        public UpdateUserDetailsHandler(IUserService userService)
        {

            _userService = userService;

        }
        public async Task<UpdateUserDetailsResponse> Handle(UpdateUserDetailsRequest request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(request);
        }
    }
}
