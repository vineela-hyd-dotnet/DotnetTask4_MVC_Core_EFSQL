
using DotnetTask4_MVC_Core_EFQL.Models;
using DotnetTask4_MVC_Core_EFSQL.Models;
using DotnetTask4_MVC_Core_EFSQL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotnetTask4_MVC_Core_EFSQL.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AttendancesController(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        //show all attendances
        public async Task<IActionResult> Index()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> Mark()
        {
            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "EmployeeId");
            ViewBag.StatusList = new SelectList(new[] { "Present", "Absent", "On Leave" });
            ViewBag.DepartmentList = new SelectList(employees.Select(e => e.Department).Distinct());

            return View();
        }

        //post:create 
        [HttpPost]
        public async Task<IActionResult> Mark(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return View(attendance);
            }

            await _attendanceRepository.AddAsync(attendance);
            return RedirectToAction("Index", "Attendances");
        }

        //get:edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
                return NotFound();

            var employees = await _employeeRepository.GetAllAsync();
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "Name");
            ViewBag.StatusList = new SelectList(new[] { "Present", "Absent", "On Leave" });
            ViewBag.DepartmentList = new SelectList(employees.Select(e => e.Department).Distinct());

            return View(attendance);
        }

        //post:edit
        [HttpPost]
        public async Task<IActionResult> Edit(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {

                var employees = await _employeeRepository.GetAllAsync();
                ViewBag.EmployeeList = new SelectList(employees, "EmployeeId", "Name");
                ViewBag.StatusList = new SelectList(new[] { "Present", "Absent", "On Leave" });
                ViewBag.DepartmentList = new SelectList(employees.Select(e => e.Department).Distinct());

                return View(attendance);
            }

            await _attendanceRepository.UpdateAsync(attendance);
            return RedirectToAction("Index", "Attendances");
        }

        //get:filtereddata
        [HttpGet]
        public async Task<IActionResult> Report(DateTime? from, DateTime? to, int? employeeId, string status)
        {
            var allAttendance = await _attendanceRepository.GetAllAsync();

            if (from.HasValue)
                allAttendance = allAttendance.Where(a => a.Date >= from.Value).ToList();

            if (to.HasValue)
                allAttendance = allAttendance.Where(a => a.Date <= to.Value).ToList();

            if (employeeId.HasValue)
                allAttendance = allAttendance.Where(a => a.EmployeeId == employeeId.Value).ToList();

            if (!string.IsNullOrEmpty(status))
                allAttendance = allAttendance.Where(a => a.Status == status).ToList();

            ViewBag.Employees = await _employeeRepository.GetAllAsync();
            return View(allAttendance);
        }
    }
        }
