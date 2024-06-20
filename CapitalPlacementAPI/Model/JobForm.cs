namespace CapitalPlacementAPI.Model
{
    public class JobForm
    {
        public string Id { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public List<Question> PersonalDetails { get; set; }
        public List<Question> AdditionalQuestions { get; set; }
    }
}
