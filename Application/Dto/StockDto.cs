using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class StockDto
    {
        public StockDto()
        {
            Ingredients = new List<IngredientDto>();
        }

        public int OrderId { get; set; }
        public int RecipeId { get; set; }
        public bool Available { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
