using Domain.Models;

namespace Repository.Interface
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeById(int recipeId);
    }
}
