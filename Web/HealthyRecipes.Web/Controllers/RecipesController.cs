namespace HealthyRecipes.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using HealthyRecipes.Common;
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Data;
    using HealthyRecipes.Services.Messaging;
    using HealthyRecipes.Web.ViewModels.Recipes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IEmailSender emailSender;

        public RecipesController(ICategoriesService categoriesService, IRecipesService recipesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
            this.environment = environment;
            this.emailSender = emailSender;
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
                await this.recipesService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();
                return this.View(input);
            }

            this.TempData["Message"] = "Recipe added successfully.";

            // TODO: Redirect to recipe info page
            return this.RedirectToAction("All");
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

        public IActionResult ById(int id)
        {
            SingleRecipeViewModel recipe = this.recipesService.GetById<SingleRecipeViewModel>(id);
            return this.View(recipe);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.recipesService.GetById<EditRecipeInputModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel recipe)
        {
            if (!this.ModelState.IsValid)
            {
                recipe.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();
                return this.View(recipe);
            }

            await this.recipesService.UpdateAsync(id, recipe);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var recipe = this.recipesService.GetById<RecipeInListVIewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{recipe.Name}</h1>");
            html.AppendLine($"<h3>{recipe.CategoryName}</h3>");
            html.AppendLine($"<img src=\"{recipe.ImageUrl}\" />");
            await this.emailSender.SendEmailAsync("email@domain.com", "HealtyRecipes", "email@gmail.com", recipe.Name, html.ToString());
            return this.RedirectToAction(nameof(this.ById), new { id });
        }
    }
}
