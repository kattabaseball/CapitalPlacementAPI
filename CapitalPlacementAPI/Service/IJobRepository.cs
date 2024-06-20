using CapitalPlacementAPI.Model;

namespace CapitalPlacementAPI.Service
{
    public interface IJobRepository
    {
        Task AddJobFormAsync(JobForm jobForm);
        Task UpdateJobFormAsync(JobForm jobForm);
        Task<JobForm> GetJobFormAsync(string id);
        Task<IEnumerable<JobForm>> GetJobFormsAsync();
    }
}
