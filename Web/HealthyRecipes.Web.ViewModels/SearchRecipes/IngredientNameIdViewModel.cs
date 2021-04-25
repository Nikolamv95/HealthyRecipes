namespace HealthyRecipes.Web.ViewModels.SearchRecipes
{
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    public class IngredientNameIdViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
