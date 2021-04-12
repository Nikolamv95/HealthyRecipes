namespace HealthyRecipes.Services.Data
{
    using HealthyRecipes.Services.Data.Models;

    public interface IGetCountsService
    {
        // 1. Use the view model
        // 2. Create DTO -> View Model -> Best way to work
        // 3. Tuples (,,,)
       CountsDto GetCounts();
    }
}
