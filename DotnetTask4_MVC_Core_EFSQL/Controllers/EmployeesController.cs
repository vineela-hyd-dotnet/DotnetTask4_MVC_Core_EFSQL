using Microsoft.AspNetCore.Mvc;
using DotnetTask4_MVC_Core_EFSQL.Models;
using DotnetTask4_MVC_Core_EFSQL.Repository;
using System.Threading.Tasks;

namespace DotnetTask4_MVC_Core_EFSQL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return View(employees);
        }

        // GET: iD
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return View(employee);
        }

        // GET: Employees/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.AddAsync(employee);
                return RedirectToAction("Index", "Employees");
            }
            return BadRequest(ModelState);
        }

        //get:update form 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        // POST: Employees/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var emp = await _employeeRepository.GetByIdAsync(employee.EmployeeId);
            if (emp == null)
            {
                return NotFound();
            }

            await _employeeRepository.UpdateAsync(employee);
            return RedirectToAction("Index","Employees");
        }


        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _employeeRepository.GetByIdAsync(id); 

            if (emp == null)
            {
                return NotFound("Employee not found");
            }

            await _employeeRepository.DeleteAsync(id);

            return RedirectToAction("Index", "Employees");
        }
    }
}


