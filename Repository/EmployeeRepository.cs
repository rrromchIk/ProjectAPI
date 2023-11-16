using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Models;

namespace ProjectAPI.Repository {
    public class EmployeeRepository : IEmployeeRepository {
        private readonly DataContext _dataContext;

        public EmployeeRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public ICollection<Employee> GetAllEmployees() {
            return _dataContext.Employees
                .Include(e => e.Tasks)
                .ToList();
        }

        public Employee GetEmployeeById(int id) {
            return _dataContext.Employees
                .Include(e => e.Tasks)
                .FirstOrDefault(e => e.Id == id);
        }

        public bool EmployeeExists(int id) {
            return _dataContext.Employees.Any(e => e.Id == id);
        }

        public bool CreateEmployee(Employee employee) {
            _dataContext.Add(employee);
            return Save();
        }

        public bool Save() {
            return _dataContext.SaveChanges() > 0;
        }

        public bool UpdateEmployee(Employee employee) {
            _dataContext.Update(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee) {
            _dataContext.Remove(employee);
            return Save();
        }
    }
}