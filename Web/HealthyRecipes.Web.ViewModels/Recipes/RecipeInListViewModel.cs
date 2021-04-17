namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using System.Linq;
    using AutoMapper;

    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    // To use the functionality of the auto mapper the class should inherits IMapFrom<tge model from the DB which we want to map> (IMapFrom<Recipe>)
    // In this case RecipeInListVIewModel will be mapped from Recipe.
    // IHaveCustomMappings is used to map properties which are difficult to be mapped from the base settings of auto mapper
    public class RecipeInListVIewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Recipe, RecipeInListVIewModel>() // From which database model, what viewModel to create
                .ForMember(x => x.ImageUrl, opt =>
                      opt.MapFrom(x =>
                          x.Images.FirstOrDefault().RemoteImageUrl != null ?
                          x.Images.FirstOrDefault().RemoteImageUrl :
                          $"/images/recipes/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extension}"));
        }
    }
}
