using ExpenseTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public interface ICategoryManagementService
    {
        //TODO : Get list of categoryId and category name to show case in Expense Management page
        // use InMemory cache to cache and event and deltegate to update the cache
        Task<bool> CreateCategory(CreateCategoryCommand request);
        Task<GetCategoriesPaginatedResponse> GetuserSpecificCategories(GetCategoriesQuery request);
        Task<bool> UpdateCategory(UpdateCategoryCommand update);
        Task<bool> DeleteCategory(DeleteCategoryCommand request);
    }
}
