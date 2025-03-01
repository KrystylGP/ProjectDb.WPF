using Data.Entities;
using Data.Interfaces;

namespace Data.Business.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;


    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public bool CreateProject(ProjectEntity projectEntity)
    {
        return _projectRepository.Create(projectEntity);
    }

    public List<ProjectEntity> GetAllProjects()
    {
        return _projectRepository.GetAll().ToList();
    }

    public ProjectEntity? GetProjectById(int projectId)
    {
        return _projectRepository.GetById(projectId);
    }

    public bool UpdateProject(ProjectEntity updatedProject)
    {
        var existingProject = _projectRepository.GetById(updatedProject.Id);

        if (existingProject == null) 
        {
            return false;
        }

        // ProjectNumber uppdateras aldrig.
        existingProject.ProjectName = updatedProject.ProjectName;
        existingProject.StartDate = updatedProject.StartDate;
        existingProject.EndDate = updatedProject.EndDate;
        existingProject.ProjectManager = updatedProject.ProjectManager;
        existingProject.Service = updatedProject.Service;
        existingProject.TotalPrice = updatedProject.TotalPrice;
        existingProject.ProjectStatus = updatedProject.ProjectStatus;

        return _projectRepository.Update(updatedProject);
    }

    public bool DeleteProject(int projectId)
    {
        return _projectRepository.Delete(projectId);
    }
}

