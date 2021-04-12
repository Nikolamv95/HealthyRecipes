namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using System;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateRecipeInputModel
    {
        [Required]
        [MinLength(4)]
        [Display(Name = "Recipe name")]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        [Display(Name = "Description")]
        public string Instructions { get; set; }

        [Range(0, 24 * 60)]
        [Display(Name = "Preparation time (in minutes)")]
        public int PreparationTime { get; set; }

        [Range(0, 24 * 60)]
        [Display(Name = "Cooking Time time (in minutes)")]
        public int CookingTime { get; set; }

        [Range(1, 100)]
        [Display(Name = "Portion Count")]
        public int PortionCount { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
