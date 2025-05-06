using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Models
{    
    [Table("tblUnitOfMeasure", Schema = "public")]
    public class UnitOfMeasure
    {
        public UnitOfMeasure()
        {
            Ingredients = new List<Ingredient>();
           // RecipeIngredients = new List<RecipeIngredient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUnitOfMeasure")]
        public int IdUnitOfMeasure { get; set; }

        [Column("IdRecipe")]
        public string Description { get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }

        //public virtual List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
