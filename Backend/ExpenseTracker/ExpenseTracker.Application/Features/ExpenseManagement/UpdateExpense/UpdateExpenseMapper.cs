using AutoMapper;
using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class UpdateExpenseMapper:Profile
    {
        public UpdateExpenseMapper()
        {
            CreateMap<UpdateExpenseCommand, Expenses>();
        }
    }
}
