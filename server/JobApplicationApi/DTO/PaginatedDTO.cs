namespace JobApplicationApi.DTO
{
    public class PaginatedDTO<T>
    {
        public IEnumerable<T> content { get; set; }
        public PaginationDTO pagination { get; set; }
    }
}