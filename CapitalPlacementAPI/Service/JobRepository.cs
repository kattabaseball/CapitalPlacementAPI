using CapitalPlacementAPI.Model;
using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.ComponentModel;


namespace CapitalPlacementAPI.Service
{
    public class JobRepository : IJobRepository
    {
        private readonly Microsoft.Azure.Cosmos.Container _container;

        public JobRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:JobFormContainer"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddJobFormAsync(JobForm jobForm)
        {
            await _container.CreateItemAsync(jobForm, new PartitionKey(jobForm.Id));
        }

        public async Task UpdateJobFormAsync(JobForm jobForm)
        {
            await _container.UpsertItemAsync(jobForm, new PartitionKey(jobForm.Id));
        }

        public async Task<JobForm> GetJobFormAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<JobForm>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<JobForm>> GetJobFormsAsync()
        {
            var query = _container.GetItemQueryIterator<JobForm>();
            var results = new List<JobForm>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
