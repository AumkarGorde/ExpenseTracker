using AutoMapper;
using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class CreateExpensesMapper : Profile
    {
        public CreateExpensesMapper()
        {
            CreateMap<CreateExpenseCommand, Expenses>();
        }
    }
}
