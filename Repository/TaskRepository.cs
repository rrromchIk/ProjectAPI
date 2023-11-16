using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;
using Task = ProjectAPI.Models.Task;

namespace ProjectAPI.Repository {
    public class TaskRepository : ITaskRepository {
        private readonly DataContext _dataContext;

        public TaskRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public ICollection<Task> GetAllTasks() {
            return _dataContext.Tasks.ToList();
        }

        public Task GetTaskById(int id) {
            return _dataContext.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public bool TaskExists(int id) {
            return _dataContext.Tasks.Any(t => t.Id == id);
        }

        public bool CreateTask(Task task) {
            _dataContext.Add(task);
            return Save();
        }

        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateTask(Task task) {
            _dataContext.Update(task);
            return Save();
        }

        public bool DeleteTask(Task task) {
            _dataContext.Remove(task);
            return Save();
        }
    }
}