using ProjectAPI.Models;

namespace ProjectAPI.Interfaces {
    public interface IProjectRepository {
        ICollection<Project> GetAllProjects();
        Project GetProjectById(int id);
        bool ProjectExists(int id);
        bool CreateProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();
    }
}


    
