
using DotnetTask4_MVC_Core_EFQL.Models;
using DotnetTask4_MVC_Core_EFSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetTask4_MVC_Core_EFSQL.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _appcontext;
        public AttendanceRepository(AppDbContext appcontext)
        {
            _appcontext = appcontext;
        }
        public async Task AddAsync(Attendance attendance)
        {
            await _appcontext.Attendances.AddAsync(attendance);
            await _appcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _appcontext.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _appcontext.Attendances.Remove(attendance);
                await _appcontext.SaveChangesAsync();
            }
        }

        public async Task<List<Attendance>> GetAllAsync()
        {
           
            return await _appcontext.Attendances
                .Include(a => a.Employee) 
                .ToListAsync();
        
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
            return await _appcontext.Attendances.FindAsync(id);
        }

        public async Task<List<Attendance>> GetFilteredAsync(DateTime? from, DateTime? to, int? employeeId, string status)
        {
            var query = _appcontext.Attendances.AsQueryable();

            if (from.HasValue)
                query = query.Where(a => a.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(a => a.Date <= to.Value);

            if (employeeId.HasValue)
                query = query.Where(a => a.EmployeeId == employeeId.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(a => a.Status == status);

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            var existing = await _appcontext.Attendances.FindAsync(attendance.AttendanceId);
            if (existing != null)
            {
                existing.CheckInTime = attendance.CheckInTime;
                existing.CheckOutTime = attendance.CheckOutTime;
                existing.Remarks = attendance.Remarks;
                existing.Date = attendance.Date;
                existing.Status = attendance.Status;
                _appcontext.Update(existing);
                await _appcontext.SaveChangesAsync();
            }

        }
    }
}