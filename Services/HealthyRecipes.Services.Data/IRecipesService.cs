namespace HealthyRecipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthyRecipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId, string imagePath);

        // I want the result of my IEnumerable<T> to be from type T in GetAll<T>.
        // The T type in GetAll<T> will be provided by the place in which the method is called.
        // Check RecipesController for example
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);

        Task DeleteAsync(int id);
    }
}
