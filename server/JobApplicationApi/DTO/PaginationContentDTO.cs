namespace JobApplicationApi.DTO
{
    public class PaginationContentDTO<T>
    {
        public IEnumerable<T> content { get; set; }
        public PaginationDTO pagination { get; set; }
    }
}