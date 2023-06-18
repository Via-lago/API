using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Client.ViewModels;
using MessagePack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeController : Controller
    {
        private readonly IEmployeRepository repository;

        public EmployeController(IEmployeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var employees = new List<Employee>();

            if (result.Data != null)
            {
                employees = result.Data?.Select(e => new Employee
                {
                    Guid = e.Guid,
                    Nik = e.Nik,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender =e.Gender,
                    HiringDate =e.HiringDate,
                    Email =e.Email,
                    PhoneNumber =e.PhoneNumber
                }).ToList();
            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Create(Employee employee)
        {
            
                var result = await repository.Post(employee);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
        
            return RedirectToAction(nameof(Index));
        }
       
        [HttpPost]
       /* [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Edit(Employee employee)
        {
         
                var result = await repository.Put(employee);
                if (result.StatusCode == "200")
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.StatusCode == "409")
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
       
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employees = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employees);
            }
            else
            {
                employees.Guid = result.Data.Guid;
                employees.Nik = result.Data.Nik;
                employees.FirstName = result.Data.FirstName;
                employees.LastName = result.Data.LastName;
                employees.BirthDate = result.Data.BirthDate;
                employees.Gender = result.Data.Gender;
                employees.HiringDate = result.Data.HiringDate;
                employees.Email = result.Data.Email;
                employees.PhoneNumber = result.Data.PhoneNumber;
                employees.CreatedDate = result.Data.CreatedDate;
                employees.ModifiedDate = DateTime.Now;
            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Delete1(Guid Guid)
        {
            var result = await repository.Get(Guid);
            var employee = new Employee();
            if (result.Data?.Guid is null)
            {
                return View(employee);
            }
            else
            {
                employee.Guid = result.Data.Guid;
                employee.Nik = result.Data.Nik;
                employee.FirstName = result.Data.FirstName;
                employee.LastName = result.Data.LastName;
                employee.BirthDate = result.Data.BirthDate;
                employee.Gender = result.Data.Gender;
                employee.HiringDate = result.Data.HiringDate;
                employee.Email = result.Data.Email;
                employee.PhoneNumber = result.Data.PhoneNumber;
                employee.CreatedDate = result.Data.CreatedDate;
                employee.ModifiedDate = DateTime.Now;
            }
            return View(employee);
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Remove(Guid Guid)
        {
            var result = await repository.Delete1(Guid);
            if (result.StatusCode == "200")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAll()
        {
            var result = await repository.GetAll();
            var employees = new List<GetAllEmployee>();

            if (result.Data != null)
            {
                employees = result.Data?.Select(e => new GetAllEmployee
                {
                    Guid = e.Guid,
                    Nik = e.Nik,
                    FullName = e.FullName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Major = e.Major,
                    Degree = e.Degree,
                    Gpa = e.Gpa,
                    UniversityName = e.UniversityName
                }).ToList();
            }

            return View(employees);
        }
    }
}
