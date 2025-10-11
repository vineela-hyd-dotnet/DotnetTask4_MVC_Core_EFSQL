

using DotnetTask4_MVC_Core_EFQL.Models;

namespace DotnetTask4_MVC_Core_EFSQL.Repository
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAllAsync();
        Task<Attendance> GetByIdAsync(int id);
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(int id);
        Task<List<Attendance>> GetFilteredAsync(DateTime? from, DateTime? to, int? employeeId, string status);
    }
}

