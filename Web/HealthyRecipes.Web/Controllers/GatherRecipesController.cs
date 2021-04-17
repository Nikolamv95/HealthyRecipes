namespace HealthyRecipes.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthyRecipes.Services;
    using Microsoft.AspNetCore.Mvc;

    // TODO: Move in administration area
    public class GatherRecipesController : BaseController
    {
        private readonly IGotvachBgScraperService gotvachBgScraperService;

        public GatherRecipesController(IGotvachBgScraperService gotvachBgScraperService)
        {
            this.gotvachBgScraperService = gotvachBgScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            await this.gotvachBgScraperService.ImportRecipesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
