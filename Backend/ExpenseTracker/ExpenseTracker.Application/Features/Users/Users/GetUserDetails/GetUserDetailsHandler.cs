using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsRequest, GetUserDetailsResponse>
    {
        private readonly IUserService _userService;
        public GetUserDetailsHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserDetailsResponse> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserDetails(request);
        }
    }
}
