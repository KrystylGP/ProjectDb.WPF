using Data.Entities;

namespace Data.Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity CreateProject(
       int customerId,
       string projectName,
       DateTime startDate,
       DateTime endDate,
       string projectManager,
       string service,
       int? totalPrice,
       string projectStatus)
    {
        return new ProjectEntity
        {
            CustomerId = customerId,
            ProjectName = projectName,
            StartDate = startDate,
            EndDate = endDate,
            ProjectManager = projectManager,
            Service = service,
            TotalPrice = totalPrice,
            ProjectStatus = projectStatus
        };
    }
}
      