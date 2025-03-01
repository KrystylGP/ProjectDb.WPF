using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository
{
    bool Create(ProjectEntity projectEntity);
    IEnumerable<ProjectEntity> GetAll();
    ProjectEntity? GetById(int projectId);
    bool Update(ProjectEntity updatedProject);
    bool Delete(int projectId); 
}