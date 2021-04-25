using HealthyRecipes.Web.ViewModels.SearchRecipes;

namespace HealthyRecipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthyRecipes.Data.Common.Repositories;
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public IEnumerable<T> GetAllPopular<T>()
        {
            return this.ingredientsRepository
                .All()
                .Where(x => x.Ingredients.Count() >= 20)
                .OrderByDescending(x=> x.Ingredients.Count())
                .To<T>()
                .ToList();
        }
    }
}
