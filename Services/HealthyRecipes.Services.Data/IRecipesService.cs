namespace HealthyRecipes.Services.Data
{
    using System.Threading.Tasks;

    using HealthyRecipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input);
    }
}
