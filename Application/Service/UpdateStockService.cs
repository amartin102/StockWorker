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

        public async Task<bool> UpdateStockAsync(IngredientDto ingredient)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                //1. Consultar el ingrediente por id
                var ingredientModel = await _ingredientRepository.GetIngredientById(ingredient.IdIngredient);
                    ingredientModel.Quantity = ingredient.Quantity;

                //2. actualizamos el stock
                var result = await _ingredientRepository.UpdateStockAsync(ingredientModel);

                if (result)
                {
                    scope.Complete();
                    return true;
                }

                scope.Dispose();
                return false;
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
