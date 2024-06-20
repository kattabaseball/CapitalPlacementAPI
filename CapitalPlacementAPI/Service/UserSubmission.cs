using CapitalPlacementAPI.Model;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacementAPI.Service
{
    public class CosmosUserSubmissionRepository : IUserSubmission
    {
        private readonly Container _container;

        public CosmosUserSubmissionRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:UserSubmissionContainer"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddUserSubmissionAsync(UserSubmission userSubmission)
        {
            await _container.CreateItemAsync(userSubmission, new PartitionKey(userSubmission.Id));
        }

        public async Task<UserSubmission> GetUserSubmissionAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<UserSubmission>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserSubmission>> GetUserSubmissionsAsync(string jobFormId)
        {
            var query = _container.GetItemQueryIterator<UserSubmission>(new QueryDefinition("SELECT * FROM c WHERE c.JobFormId = @jobFormId").WithParameter("@jobFormId", jobFormId));
            var results = new List<UserSubmission>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
