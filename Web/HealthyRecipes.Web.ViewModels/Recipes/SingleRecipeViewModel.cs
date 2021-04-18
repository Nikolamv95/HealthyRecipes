using System.Collections.Generic;

namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Linq;

    using AutoMapper;
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    public class SingleRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        // In order to map the UserName of the recipe owner the name of the property should be
        // Recipe -> ApplicationUser AddedByUser and UserName (which is the name of the property in IdentityUser class)
        public string AddedByUserUserName { get; set; }

        // AutoMapper also supports aggregated function like Count, Sum and etc... This feature will be available
        // if we provide the name of the Model (CategoryRecipes) and the aggregated function (Count)
        public int CategoryRecipesCount { get; set; }

        public string ImageUrl { get; set; }

        public string OriginalUrl { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public double VotesAverageValue { get; set; }

        public IEnumerable<IngredientsViewModel> Ingredients { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            // From which database model, what viewModel to create
            configuration
                .CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.VotesAverageValue, opt => opt.MapFrom(x => x.Votes.Average(x => x.Value)))
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.FirstOrDefault().RemoteImageUrl != null ?
                            x.Images.FirstOrDefault().RemoteImageUrl :
                            $"/images/recipes/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extension}"));
        }
    }
}
