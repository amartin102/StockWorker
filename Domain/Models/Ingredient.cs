using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("tblIngredient", Schema = "public")]
    public class Ingredient
    {
        public Ingredient()
        {
            Ingredients = new List<RecipeIngredient>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdIngredient")]
        public int IdIngredient { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Ammount")]
        public int Quantity { get; set; }

        [Column("IdUnitOfMeasure")]
        public int IdUnitOfMeasure { get; set; }

        public virtual List<RecipeIngredient> Ingredients { get; set; }

        public virtual UnitOfMeasure UnitOfMeasureIngredient { get; set; }
    }
}
