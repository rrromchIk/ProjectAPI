using Task = ProjectAPI.Models.Task;

namespace ProjectAPI.Interfaces {
    public interface ITaskRepository {
        ICollection<Task> GetAllTasks();
        Task GetTaskById(int id);
        bool TaskExists(int id);
        bool CreateTask(Task task);
        bool UpdateTask(Task task);
        bool DeleteTask(Task task);
        bool Save();
    }
}