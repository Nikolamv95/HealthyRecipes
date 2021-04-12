namespace HealthyRecipes.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        /// <summary>
        /// Get all categories as key value pairs. Key is the Id and Value is the Name of the category.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs();
    }
}
