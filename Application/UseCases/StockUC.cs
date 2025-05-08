using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using Application.UseCases.Interface;
using Domain.Models;
using Microsoft.Extensions.Hosting;

namespace Application.UseCases
{
    public class StockUC: IStockUC
    {
        private readonly ICheckAvailabilityService _checkAvailabilityService;
        private readonly IUpdateStockService _updateStockService;
        

        public StockUC(ICheckAvailabilityService checkAvailabilityService , IUpdateStockService updateStockService) 
        {            
            _checkAvailabilityService = checkAvailabilityService;
            _updateStockService = updateStockService;
        }

        public async Task<StockDto> GetRecipeById(int recipeId, int orderId)
        {
            var recipe = await _checkAvailabilityService.GetRecipeById(recipeId, orderId);

            return recipe;
        }

        public async Task<bool> UpdateStockAsync(IngredientDto ingredient)
        {
            var recipe = await _updateStockService.UpdateStockAsync(ingredient);
            return recipe;
        }

    }
}
