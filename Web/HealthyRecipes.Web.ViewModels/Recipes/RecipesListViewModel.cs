namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HealthyRecipes.Web.ViewModels.Global;

    public class RecipesListViewModel : PagingViewModel
    {
        public IEnumerable<RecipeInListVIewModel> Recipes { get; set; }
    }
}
