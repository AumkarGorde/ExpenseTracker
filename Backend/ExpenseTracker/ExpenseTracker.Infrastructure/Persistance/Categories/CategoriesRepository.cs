using ExpenseTracker.Application;
using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public class CategoriesRepository : Repository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(ExpenseTrackerDBContext dBContext) : base(dBContext)
        {
            
        }
        public async Task<GetCategoriesPaginatedResponse> GetuserSpecificCategories(Guid userId, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            var query = _entities.Where(c => (c.UserId == userId || c.IsDefault) && (c.IsDefault == true || c.IsDefault == false));
            var totalItems = await query.CountAsync();
            var categories = await query
                            .Skip(skip)
                            .Take(pageSize)
                            .Select(c=> new GetCategoriesResponse()
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName,
                                CategoryDescription = c.CategoryDescription,
                                IsDefault = c.IsDefault    
                            }).ToListAsync();


            return new GetCategoriesPaginatedResponse()
            {
                Categories = categories,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
