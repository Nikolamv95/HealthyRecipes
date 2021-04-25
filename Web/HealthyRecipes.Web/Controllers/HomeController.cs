namespace HealthyRecipes.Web.Controllers
{
    using System.Diagnostics;

    using HealthyRecipes.Services.Data;
    using HealthyRecipes.Web.ViewModels;
    using HealthyRecipes.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;
        private readonly IRecipesService recipesService;

        // private readonly IMapper mapper -> Implement IMapper mapper in the constructor

        // 1. Work ApplicationDBContext
        // 2. Work IRepository
        // 3. Work with Services -> Best way to work
        public HomeController(IGetCountsService countsService, IRecipesService recipesService)
        {
            this.countsService = countsService;
            this.recipesService = recipesService;
        }

        public IActionResult Index()
        {
            // Create viewModel with DTOs
            var countsDto = this.countsService.GetCounts();
            var viewModel = new IndexViewModel()
            {
                CategoriesCount = countsDto.CategoriesCount,
                ImagesCount = countsDto.ImagesCount,
                IngredientsCount = countsDto.IngredientsCount,
                RecipesCount = countsDto.RecipesCount,
                RandomRecipes = this.recipesService.GetRandom<IndexPageRecipeViewModel>(10),
            };

            // or with AutoMapper
            // var viewModelAutoMap = this.mapper.Map<IndexViewModel>(countsDto);
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
