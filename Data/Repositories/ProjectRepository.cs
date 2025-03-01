using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        // Tar emot DataContext via Dependency.
        public ProjectRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ProjectRepository()
        {
        }

        // CREATE
        public bool Create(ProjectEntity projectEntity)
        {
            int startNumber = _context.Projects.Any() ? _context.Projects.Max(p => p.ProjectNumber) : 100; // Hämtar högsta p.nummer, om ej null incr. annars 100.
            projectEntity.ProjectNumber = startNumber + 1; // Tilldela ny 'ProjectNumber'

            _context.Projects.Add(projectEntity);
            return _context.SaveChanges() > 0;
        }

        // READ
        public IEnumerable<ProjectEntity> GetAll()
        {
            return _context.Projects
                .Include(p => p.CustomerInfo)
                .ToList();
        }

        public ProjectEntity? GetById(int projectid)
        {
            return _context.Projects
                .Include(p => p.CustomerInfo)
                .FirstOrDefault(p => p.Id == projectid);
        }

        // UPDATE
        public bool Update(ProjectEntity projectEntity)
        {
            _context.Projects.Update(projectEntity);
            return _context.SaveChanges() > 0;
        }

        // DELETE
        public bool Delete(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            return _context.SaveChanges() > 0;
        }
    }
}
