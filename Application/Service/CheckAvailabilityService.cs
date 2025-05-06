using Application.Interface;
using Domain.Models;
using Repository.Interface;

namespace Application.Service
{
   public class CheckAvailabilityService : ICheckAvailabilityService
    {

        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public CheckAvailabilityService(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository) {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<bool> GetRecipeById(int recipeId)
        {            
            var recipe = await _recipeRepository.GetRecipeById(recipeId);

            if (recipe != null)
            {
                //Consultar los ingredientes de la receta
                var allIngredients = _ingredientRepository.GetAllIngredients();

                //Comparar la medida de cada ingrediente requerido con el stock disponible de ingredientes
                foreach (var ingredient in recipe.RecipeIngredients)
                {
                    var ingredientStock = allIngredients.Result.FirstOrDefault(i => i.IdIngredient == ingredient.IdIngredient);
                    if (ingredientStock != null && ingredient.Quantity > ingredientStock.Quantity)
                    {
                        return false; //hay suficiente stock
                    }
                }

                return true;
            }

            return false;
        }
    }
}
