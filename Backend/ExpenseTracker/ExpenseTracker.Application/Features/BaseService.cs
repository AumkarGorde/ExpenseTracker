using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class BaseService
    {
        public readonly ICustomLogger _logger;
        public readonly IMapper _mapper;
        public BaseService(ICustomLogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}
