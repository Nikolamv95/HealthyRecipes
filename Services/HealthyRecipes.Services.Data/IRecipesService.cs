using System.Collections.Generic;

namespace HealthyRecipes.Services.Data
{
    using System.Threading.Tasks;

    using HealthyRecipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId);

        // I want the result of my IEnumerable<T> to be from type T in GetAll<T>.
        // The T type in GetAll<T> will be provided by the place in which the method is called.
        // Check RecipesController for example
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();
    }
}
