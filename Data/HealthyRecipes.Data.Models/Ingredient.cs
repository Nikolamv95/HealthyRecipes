namespace HealthyRecipes.Data.Models
{
    using System.Collections.Generic;

    using HealthyRecipes.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Ingredients = new HashSet<RecipeIngredient>();
        }

        public string Name { get; set; }

        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}
