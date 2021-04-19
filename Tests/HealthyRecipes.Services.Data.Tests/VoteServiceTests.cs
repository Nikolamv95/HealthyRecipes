namespace HealthyRecipes.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;

    using HealthyRecipes.Data.Common.Repositories;
    using HealthyRecipes.Data.Models;
    using Moq;
    using Xunit;

    public class VoteServiceTests
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnly1VoteShouldBeCounted()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 1);
            await service.SetVoteAsync(1, "1", 2);
            await service.SetVoteAsync(1, "1", 3);
            await service.SetVoteAsync(1, "1", 4);
            await service.SetVoteAsync(1, "1", 5);

            Assert.Single(list);
            Assert.Equal(5, list.First().Value);
        }

        [Fact]
        public async Task When2UserVotesForSameRecipeTheAverageVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(2, "Nikola", 5);
            await service.SetVoteAsync(2, "Ivan", 1);
            await service.SetVoteAsync(2, "Nikola", 2);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Exactly(2));

            Assert.Equal(2, list.Count());
            Assert.Equal(1.5, service.GetAverageVotes(2));
        }
    }
}
