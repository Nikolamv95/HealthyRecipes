namespace HealthyRecipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HealthyRecipes.Data.Common.Repositories;
    using HealthyRecipes.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        /// <summary>
        /// Get categories as key value pair.
        /// </summary>
        /// <returns>IEnumerable of key value pairs where Key is the Id and Value is the name.</returns>
        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs()
        {
            // Firstly we extract all the categories data with Id and name with ToList
            // (in order start work on the local level without using the database anymore),
            // then we create new KeyValuePair in which we add the Id and the name which we already extracted from the database
            return this.categoriesRepository
                .AllAsNoTrackingWithDeleted()
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
