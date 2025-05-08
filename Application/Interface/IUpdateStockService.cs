using Application.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUpdateStockService
    {
        Task<bool> UpdateStockAsync(List<IngredientDto> ingredients);
    }
}
