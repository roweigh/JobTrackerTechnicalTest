namespace JobApplicationApi.DTO
{
    public class PaginationDTO
    {
        public int page { get; set; }
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public bool first { get; set; }
        public bool last { get; set; }
    }
}