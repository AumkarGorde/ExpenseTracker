using ExpenseTracker.Application;
using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    internal class ExpensesRepository : Repository<Expenses>, IExpensesRepository
    {
        public ExpensesRepository(ExpenseTrackerDBContext dBContext) : base(dBContext)
        {
        }

        public async Task<GetExpensePaginatedResponse> GetExpenseByUser(Guid userId, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var query = _entities.Where(ex => ex.UserId == userId)
                        .OrderByDescending(e => e.Date);

            var totalItems = await query.CountAsync();

            var expenses = await query
                           .Skip(skip)
                           .Take(pageSize)
                           .Select(e => new GetExpensesResponse()
                           {
                               ExpenseId = e.ExpenseId,
                               Description = e.Description,
                               Amount = e.Amount,
                               Date = e.Date,
                               ExpenseType = e.ExpenseType,
                               CategoryId = e.CategoryId,
                               CategoryName = e.Category.CategoryName
                           }).ToListAsync();

            return new GetExpensePaginatedResponse()
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Expenses = expenses
            };
        }

        public long GetExpenseByUserForSpecificPeriod(DateTime startDate, DateTime endTime, Guid userId, bool isExpense = false)
        {
            if (isExpense)
                return _entities
                            .Where(e => e.UserId == userId && e.ExpenseType == ExpenseType.Expenditure && e.Date >= startDate && e.Date <= e.Date)
                            .Sum(e => e.Amount);
            else
                return _entities
                            .Where(e => e.UserId == userId && e.ExpenseType == ExpenseType.Income && e.Date >= startDate && e.Date <= e.Date)
                            .Sum(e => e.Amount);
        }

        public async Task<IEnumerable<Expenses>> GetWeeklyExpenseIncomeReport(Guid userId, int Year, int Month)
        {
            return await _entities
                        .Where(e => e.UserId == userId && e.Date.Year == Year && e.Date.Month == Month)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Expenses>> GetCategoryExpenseIncomeReport(Guid userId, int Year, int Month, bool isExpense)
        {
            if (isExpense)
                return await _entities
                        .Include(e => e.Category)
                        .Where(e => e.UserId == userId && e.Date.Year == Year && e.Date.Month == Month && e.ExpenseType == ExpenseType.Expenditure)
                        .ToListAsync();
            else
                return await _entities
                        .Include(e => e.Category)
                        .Where(e => e.UserId == userId && e.Date.Year == Year && e.Date.Month == Month)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Expenses>> GetExpensePerCategoryEachUser(Guid userId, Guid categoryId)
        {
            return await _entities.Where(e => e.UserId == userId && e.CategoryId == categoryId).ToListAsync();
        }
    }
}
