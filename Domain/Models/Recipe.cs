using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("tblRecipe", Schema = "public")]
    public class Recipe
    {
        public Recipe()
        {
            RecipeIngredients = new List<RecipeIngredient>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdRecipe")]
        public int IdRecipe { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("PreparationTime")]
        public TimeSpan PreparationTime { get; set; }

        [Column("State")]
        public bool State { get; set; }

        public virtual List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
