using AutoMapper;
using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public sealed class GetCategoriesMapper: Profile
    {
        public GetCategoriesMapper()
        {
            CreateMap<Categories, GetCategoriesResponse>();
        }
    }
}
