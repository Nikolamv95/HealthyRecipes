using System.Collections.Generic;
using HealthyRecipes.Web.ViewModels.Recipes;

namespace HealthyRecipes.Web.Controllers
{
    using HealthyRecipes.Services.Data;
    using HealthyRecipes.Web.ViewModels.SearchRecipes;
    using Microsoft.AspNetCore.Mvc;

    public class SearchRecipesController : BaseController
    {
        private readonly IRecipesService recipesService;
        private readonly IIngredientsService ingredientsService;

        public SearchRecipesController(IRecipesService recipesService, IIngredientsService ingredientsService)
        {
            this.recipesService = recipesService;
            this.ingredientsService = ingredientsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new SearchIngredientsIndexViewModels()
            {
                Ingredients = this.ingredientsService.GetAllPopular<IngredientNameIdViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult List(SearchListInputModel input)
        {
            var viewModel = new ListViewModel
            {
                Recipes = this.recipesService.GetByIngredients<RecipeInListVIewModel>(input.Ingredients),
            };

            return this.View(viewModel);
        }
    }
}
