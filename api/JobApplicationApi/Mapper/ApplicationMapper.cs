using JobApplicationApi.DTO;
using JobApplicationApi.Models;

public static class ApplicationMapper
{
    public static ApplicationDTO ToDTO(Application application)
    {
        return new ApplicationDTO
        {
            id = application.id,
            companyName = application.companyName,
            position = application.position,
            status = application.status,
            dateApplied = application.dateApplied
        };
    }

    public static Application ToEntity(ApplicationDTO dto)
    {
        return new Application
        {
            id = dto.id,
            companyName = dto.companyName,
            position = dto.position,
            status = dto.status,
            dateApplied = dto.dateApplied
        };
    }
}