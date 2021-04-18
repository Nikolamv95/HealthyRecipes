using System;
using System.Security.Claims;
using HealthyRecipes.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public RecipesController(ICategoriesService categoriesService, IRecipesService recipesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [HttpGet]
        [Authorize] // Restrict from users which are not logged-in
        public IActionResult Create()
        {
            // Extract the data from the service and write it in the input model
            var categoryItems = new CreateRecipeInputModel { CategoriesItems = this.categoriesService.GetAllKeyValuePairs() };

            return this.View(categoryItems);
        }

        [HttpPost]
        [Authorize] // Restrict from users which are not logged-in
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();
                return this.View(input);
            }

            // Get userId from 1. Claim, 2. UserManager
            // var userid = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // or
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.recipesService.CreateAsync(input, user.Id, $"{this.environment.ContentRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();
                return this.View(input);
            }

            // TODO: Redirect to recipe info page
            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new RecipesListViewModel()
            {
                PageNumber = id,

                // In GetAll method we give the input model which we want to visualize and present in the cshtml page
                // We can use same method but for different ViewModels because of the <T> and auto mapper functionality
                Recipes = this.recipesService.GetAll<RecipeInListVIewModel>(id, 12),
                RecipesCount = this.recipesService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };

            if (id > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
