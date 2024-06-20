namespace CapitalPlacementAPI.Model
{
    public class UserSubmission
    {
        public string Id { get; set; }
        public string JobFormId { get; set; }
        public Dictionary<string, string> PersonalDetails { get; set; }
        public Dictionary<string, string> AdditionalQuestions { get; set; }
    }
}
