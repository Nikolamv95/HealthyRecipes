namespace HealthyRecipes.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthyRecipes.Services.Data;
    using HealthyRecipes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;

        public RecipesController(ICategoriesService categoriesService, IRecipesService recipesService)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Extract the data from the service and write it in the input model
            var viewModel = new CreateRecipeInputModel { CategoriesItems = this.categoriesService.GetAllKeyValuePairs() };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();
                return this.View(input);
            }

            await this.recipesService.CreateAsync(input);

            // TODO: Redirect to recipe info page
            return this.Redirect("/");
        }
    }
}
