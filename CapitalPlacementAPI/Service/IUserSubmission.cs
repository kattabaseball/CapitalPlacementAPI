using CapitalPlacementAPI.Model;

namespace CapitalPlacementAPI.Service
{
    public interface IUserSubmission
    {
        Task AddUserSubmissionAsync(UserSubmission userSubmission);
        Task<UserSubmission> GetUserSubmissionAsync(string id);
        Task<IEnumerable<UserSubmission>> GetUserSubmissionsAsync(string jobFormId);
    }
}
