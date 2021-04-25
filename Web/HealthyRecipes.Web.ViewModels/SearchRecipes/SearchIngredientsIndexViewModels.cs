namespace HealthyRecipes.Web.ViewModels.SearchRecipes
{
    using System.Collections.Generic;

    public class SearchIngredientsIndexViewModels
    {
        public IEnumerable<IngredientNameIdViewModel> Ingredients { get; set; }
    }
}
