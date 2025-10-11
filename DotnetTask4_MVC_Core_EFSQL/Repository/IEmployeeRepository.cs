using DotnetTask4_MVC_Core_EFSQL.Models;

namespace DotnetTask4_MVC_Core_EFSQL.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
