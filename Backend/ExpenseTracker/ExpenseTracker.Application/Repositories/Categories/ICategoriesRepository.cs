using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface ICategoriesRepository : IRepository<Categories>
    {
        Task<GetCategoriesPaginatedResponse> GetuserSpecificCategories(Guid userId, int page, int pageSize);
    }
}
