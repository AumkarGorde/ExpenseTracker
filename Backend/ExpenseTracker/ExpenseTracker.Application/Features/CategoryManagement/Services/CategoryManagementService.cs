using AutoMapper;
using ExpenseTracker.Domain;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application
{
    public class CategoryManagementService : BaseService, ICategoryManagementService
    {
        private readonly ICategoryManagementUnitOfWork _categoryManagementUnitOfWork;
        private readonly IExpenseBudgetUnitOfWork _expenseBudgetUnitOfWork;
        public CategoryManagementService(ICustomLogger logger, IMapper mapper,
            ICategoryManagementUnitOfWork categoryManagementUnitOfWork, IExpenseBudgetUnitOfWork expenseBudgetUnitOfWork ) :base(logger, mapper)
        {
            _categoryManagementUnitOfWork = categoryManagementUnitOfWork;
            _expenseBudgetUnitOfWork = expenseBudgetUnitOfWork;
        }
        public async Task<bool> CreateCategory(CreateCategoryCommand request)
        {
            try
            {
                var category = new Categories()
                {
                    CategoryName = request.CategoryName,
                    CategoryDescription = request.CategoryDescription,
                    UserId = request.UserId,
                    IsDefault = request.IsDefault,
                };
                var categoryUser = new CategoryUser()
                {
                    UserId = request.UserId,
                    Category = category
                };
                category.CategoryUsers = new List<CategoryUser> { categoryUser };


                await _categoryManagementUnitOfWork.CategoriesRepository.AddAsync(category);
                await _categoryManagementUnitOfWork.SaveChanges();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(DeleteCategoryCommand request)
        {
            try
            {
                
                var category = await _categoryManagementUnitOfWork.CategoriesRepository.GetByIdAsync(request.CategoryId);
                if (category is not null) 
                {
                    var expenses = await _expenseBudgetUnitOfWork.ExpensesRepository.GetExpensePerCategoryEachUser(category.UserId, category.CategoryId);
                    var budget = await _expenseBudgetUnitOfWork.BudgetRepository.GetBudgetByUserId(category.UserId);
                    var amountExpense = expenses.Where(e => e.ExpenseType == ExpenseType.Expenditure).Sum(e => e.Amount);
                    var amountIncome = expenses.Where(e => e.ExpenseType == ExpenseType.Income).Sum(e => e.Amount);
                    budget.Balance = budget.Balance + amountExpense - amountIncome;
                    _expenseBudgetUnitOfWork.BudgetRepository.Update(budget);
                    await _expenseBudgetUnitOfWork.SaveChanges();
                    _categoryManagementUnitOfWork.CategoriesRepository.Delete(category);
                    await _categoryManagementUnitOfWork.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<GetCategoriesPaginatedResponse> GetuserSpecificCategories(GetCategoriesQuery request)
        {
            try
            {
                return await _categoryManagementUnitOfWork.CategoriesRepository
                            .GetuserSpecificCategories(request.UserId,request.Page, request.PageSize);
            }
            catch (Exception)
            {
                return new GetCategoriesPaginatedResponse()
                {
                    Page = 1,
                    PageSize = request.PageSize,
                    TotalItems = 0,
                    Categories = new List<GetCategoriesResponse>() { }
                };
            }
        }

        public async Task<bool> UpdateCategory(UpdateCategoryCommand update)
        {
            try
            {
                var category = await _categoryManagementUnitOfWork.CategoriesRepository.GetByIdAsync(update.CategoryId);   
                if(category is null)
                    return false;
                _mapper.Map(update,category);
                _categoryManagementUnitOfWork.CategoriesRepository.Update(category);
                await _categoryManagementUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
