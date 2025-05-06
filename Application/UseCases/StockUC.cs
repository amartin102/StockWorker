using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.UseCases.Interface;
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

        public async Task<bool> GetRecipeById(int recipeId)
        {
            var recipe = await _checkAvailabilityService.GetRecipeById(recipeId);

            return recipe;
        }
    }
}
