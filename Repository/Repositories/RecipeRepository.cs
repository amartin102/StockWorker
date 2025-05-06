using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Coconseconsentext;
using Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace Repository.Repositories
{
    [ExcludeFromCodeCoverage]
    public class RecipeRepository : Repository<Recipe, StockDb>, IRecipeRepository
    {
        public RecipeRepository(StockDb context) : base(context)
        {
        }

        public async Task<Recipe> GetRecipeById(int recipeId)
        {
            //var result = await GetByIdAsync(orderId, p => p.Include(p => p.Client).Include(p => p.OrderItems).ThenInclude(p => p.Item));
            var result = await GetByIdAsync(recipeId, true, p => p.Include(p => p.RecipeIngredients));
            return result;
        }
    }
}
