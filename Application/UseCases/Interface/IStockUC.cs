using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Interface
{
    public interface IStockUC
    {
        Task<bool> GetRecipeById(int recipeId);
    }
}
