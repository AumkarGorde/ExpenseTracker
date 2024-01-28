using AutoMapper;
using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class GetExpensesMapper : Profile
    {
        public GetExpensesMapper()
        {
            CreateMap<Expenses, GetExpensesResponse>();
        }
    }
}
