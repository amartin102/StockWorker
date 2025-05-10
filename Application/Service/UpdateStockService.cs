using Application.Dto;
using Application.Interface;
using Domain.Models;
using Repository.Interface;
using System.Transactions;

namespace Application.Service
{
   public class UpdateStockService : IUpdateStockService
    {       
        private readonly IIngredientRepository _ingredientRepository;

        public UpdateStockService( IIngredientRepository ingredientRepository) {          
            _ingredientRepository = ingredientRepository;
        }

        public async Task<bool> UpdateStockAsync(List<IngredientDto> ingredients)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                List<Ingredient> ingredientList = new List<Ingredient>();
                foreach (var item in ingredients)
                {
                    //1. Consultar el ingrediente por id
                    var ingredientModel = await _ingredientRepository.GetIngredientById(item.IdIngredient);
                    ingredientModel.Quantity = (ingredientModel.Quantity - item.Quantity);

                    ingredientList.Add(ingredientModel);
                }

                //2. actualizamos el stock
                var result = await _ingredientRepository.UpdateStockAllAsync(ingredientList);
                if (!result)
                {
                    scope.Dispose();
                    return false;
                }
                scope.Complete();
                return true;
            }
            catch (Exception ex)
            {
                scope.Dispose();
                return false;
                throw;
            }
        }
    }
}
