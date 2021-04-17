namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    // This class is similar to RecipeInListViewModel to show that one method GetAll<T> can be
    // used by different ViewModels like RecipeInListViewModel which are related to Recipe model
    public class RecipeAutoMapperExampleViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
