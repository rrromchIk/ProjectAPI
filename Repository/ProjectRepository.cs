using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Repository {
    public class ProjectRepository : IProjectRepository {
        private readonly DataContext _dataContext;

        public ProjectRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public ICollection<Project> GetAllProjects() {
            return _dataContext.Projects.Include(p => p.Tasks).ToList();
        }

        public Project GetProjectById(int id) {
            return _dataContext.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);
        }

        public bool ProjectExists(int id) {
            return _dataContext.Projects.Any(p => p.Id == id);
        }

        public bool CreateProject(Project project) {
            _dataContext.Add(project);
            return Save();
        }

        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateProject(Project project) {
            _dataContext.Update(project);
            return Save();
        }

        public bool DeleteProject(Project project) {
            _dataContext.Remove(project);
            return Save();
        }
    }
}
