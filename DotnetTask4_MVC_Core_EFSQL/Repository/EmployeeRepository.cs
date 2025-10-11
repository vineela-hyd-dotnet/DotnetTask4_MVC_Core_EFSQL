using DotnetTask4_MVC_Core_EFSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTask4_MVC_Core_EFSQL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        

        public async Task AddAsync(Employee employee)
        {
           await _appDbContext.AddAsync(employee);

            await _appDbContext.SaveChangesAsync();
        }

        public async  Task DeleteAsync(int id)
        {
           var emp=await _appDbContext.Employees.FindAsync(id);
            if (emp != null)
            {
                _appDbContext.Employees.Remove(emp);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var emp = await _appDbContext.Employees.FindAsync(id);
            if(emp != null)
            {
                return emp;
            }
            return null;
        }

        public async Task UpdateAsync(Employee employee)
        {
            var emp=await _appDbContext.Employees.FindAsync(employee.EmployeeId);
            if (emp != null)
            {
                emp.Email = employee.Email;
                emp.DateOfJoining = employee.DateOfJoining;
                emp.Designation = employee.Designation;
                emp.Department = employee.Department;
                emp.Name = employee.Name;
                emp.IsActive = employee.IsActive;
                _appDbContext.Update(emp);
               await _appDbContext.SaveChangesAsync();
            }
            
        }
    }
}
