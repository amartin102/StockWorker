using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("tblRecipeIngredient", Schema = "public")]
    public class RecipeIngredient
    {
        public RecipeIngredient()
        {           
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdRecipeIngredient")]
        public int IdRecipeIngredient { get; set; }

        [Column("IdRecipe")]
        public int IdRecipe { get; set; }

        [Column("IdIngredient")]
        public int IdIngredient { get; set; }

        [Column("Ammount")]
        public int Quantity { get; set; }

        [Column("IdUnitOfMeasure")]
        public int IdUnitOfMeasure { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }

        //public virtual UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
