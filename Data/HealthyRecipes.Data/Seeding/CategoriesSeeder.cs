
using HealthyRecipes.Data.Models;

namespace HealthyRecipes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category()
            {
                Name = "Тарт",
            });

            await dbContext.Categories.AddAsync(new Category()
            {
                Name = "Кекс",
            });

            await dbContext.Categories.AddAsync(new Category()
            {
                Name = "Печено Свинско",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
