

namespace HealthyRecipes.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using HealthyRecipes.Data;
    using HealthyRecipes.Web.ViewModels;
    using HealthyRecipes.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {

            var viewModel = new IndexViewModel()
            {
                CategoriesCount = this.db.Categories.Count(),
                ImagesCount = this.db.Images.Count(),
                IngredientsCount = this.db.Ingredients.Count(),
                RecipesCount = this.db.Recipes.Count(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
