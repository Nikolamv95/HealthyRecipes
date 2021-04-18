namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        // In order to auto map the Ingredient Name from Ingredient model the property name
        // should be combination from ModelName + PropertyName (Ingredient + Name)
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
