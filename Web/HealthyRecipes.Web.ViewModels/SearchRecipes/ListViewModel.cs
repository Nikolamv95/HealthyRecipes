namespace HealthyRecipes.Web.ViewModels.SearchRecipes
{
    using System.Collections.Generic;

    using HealthyRecipes.Web.ViewModels.Recipes;

    public class ListViewModel
    {
        public IEnumerable<RecipeInListVIewModel> Recipes { get; set; }
    }
}
