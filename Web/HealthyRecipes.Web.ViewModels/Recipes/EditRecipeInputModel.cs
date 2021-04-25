using AutoMapper;

namespace HealthyRecipes.Web.ViewModels.Recipes
{
    using HealthyRecipes.Data.Models;
    using HealthyRecipes.Services.Mapping;

    public class EditRecipeInputModel : BaseRecipeInputModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, EditRecipeInputModel>()
                .ForMember(x => x.CookingTime, opt => opt.MapFrom(x => x.CookingTime.Minutes))
                .ForMember(x => x.PreparationTime, opt => opt.MapFrom(x => x.PreparationTime.Minutes))
                .ForMember(x => x.PortionCount, opt => opt.MapFrom(x => x.PortionsCount));
        }
    }
}
