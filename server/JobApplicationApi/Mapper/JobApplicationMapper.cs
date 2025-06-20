using JobApplicationApi.DTO;
using JobApplicationApi.Models;

public static class JobApplicationMapper
{
    public static JobApplicationDTO ToDTO(JobApplication application)
    {
        return new JobApplicationDTO
        {
            id = application.id,
            companyName = application.companyName,
            position = application.position,
            status = application.status,
            dateApplied = application.dateApplied
        };
    }

    public static JobApplication ToEntity(JobApplicationDTO dto)
    {
        return new JobApplication
        {
            id = dto.id,
            companyName = dto.companyName,
            position = dto.position,
            status = dto.status,
            dateApplied = dto.dateApplied
        };
    }
}