using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Coconseconsentext;
using Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace Repository.Repositories
{
    [ExcludeFromCodeCoverage]
    public class IngredientRepository : Repository<Ingredient, StockDb>, IIngredientRepository
    {
        public IngredientRepository(StockDb context) : base(context)
        {
        }

        public async Task<Ingredient> GetIngredientById(int ingredientId)
        {
            //var result = await GetByIdAsync(orderId, p => p.Include(p => p.Client).Include(p => p.OrderItems).ThenInclude(p => p.Item));
            var result = await GetByIdAsync(ingredientId, false);
            return result;
        }

        public async Task<List<Ingredient>> GetAllIngredients()
        {            
            var result = await GetAllAsync();
            return result;
        }

        public async Task<bool> UpdateStockAsync(Ingredient ingredient)
        {
            var result = await UpdateAsync(ingredient);
            return result;
        }
    }

}
