using Domain.Models;

namespace Repository.Interface
{
    public interface IIngredientRepository
    {
        Task<Ingredient> GetIngredientById(int ingredientId);

        Task<List<Ingredient>> GetAllIngredients();

        Task<bool> UpdateStockAsync(Ingredient ingredient);
    }
}
