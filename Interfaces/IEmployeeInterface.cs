using ProjectAPI.Models;

namespace ProjectAPI.Interfaces {
    public interface IEmployeeRepository {
        ICollection<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        bool EmployeeExists(int id);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool Save();
    }
}