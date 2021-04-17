using System.Collections.Generic;
using HealthyRecipes.Services.Mapping;

namespace HealthyRecipes.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyRecipes.Data.Common.Repositories;
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId)
        {
            var recipe = new Recipe()
            {
                Name = input.Name,
                Instructions = input.Instructions,
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                PortionsCount = input.PortionCount,
                CategoryId = input.CategoryId,
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.ingredientsRepository.All().FirstOrDefault(x => x.Name == inputIngredient.Name);
                if (ingredient == null)
                {
                    ingredient = new Ingredient()
                    {
                        Name = inputIngredient.Name,
                    };
                }

                recipe.Ingredients.Add(new RecipeIngredient()
                {
                    Ingredient = ingredient,
                    Quantity = inputIngredient.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all recipes from the database ordered by descending.
        /// This method can be used by different ViewModels which
        /// will be related to Recipe model form the database.
        /// </summary>
        /// <param name="page">Current page from the pagination.</param>
        /// <param name="itemsPerPage">12 is default value.</param>
        /// <returns>IEnumerable<T></T></returns>
        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            // public IEnumerable<ViewModel> GetAll<ViewModel>
            // This query is created with AutoMapper. Check RecipeInListViewModel method CreateMappings
            // Get all recipes and map them .TO<ViewModel>
            var recipes = this.recipesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return recipes;
        }

        public int GetCount()
        {
            return this.recipesRepository.All().Count();
        }
    }
}
