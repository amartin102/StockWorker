using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class IngredientDto
    {        
        public int IdIngredient { get; set; }
         
        public int Quantity { get; set; }
    }
}
