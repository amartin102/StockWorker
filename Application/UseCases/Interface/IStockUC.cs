using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Models;

namespace Application.UseCases.Interface
{
    public interface IStockUC
    {
        Task<StockDto> GetRecipeById(int recipeId, int orderId);
        Task<bool> UpdateStockAsync(IngredientDto ingredient);
    }
}
