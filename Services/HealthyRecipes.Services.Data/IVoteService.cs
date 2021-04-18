using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyRecipes.Services.Data
{
    public interface IVotesService
    {
        Task SetVoteAsync(int recipeId, string userId, byte value);

        double GetAverageVotes(int recipeId);
    }
}
