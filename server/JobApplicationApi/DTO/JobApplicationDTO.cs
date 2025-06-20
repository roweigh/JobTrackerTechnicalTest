namespace JobApplicationApi.DTO
{
    public class JobApplicationDTO
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string position { get; set; }
        public string status { get; set; }
        public DateTime dateApplied { get; set; }
    }
}