using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using API.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Universities;
using API.Repositories;
using API.ViewModels.Accounts;
using System.Net.Mail;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _emplloyeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper<Employee, EmployeeVM> _mapper;
    public EmployeeController(IEmployeeRepository employeeRepository,
                                IMapper<Employee, EmployeeVM> mapper,
                                IAccountRepository accountRepository)
    {
        _emplloyeeRepository = employeeRepository;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _emplloyeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound();
        }
        var data = employees.Select(_mapper.Map).ToList();
        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _emplloyeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(employee);
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var result = _emplloyeeRepository.Create(employeeConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    [HttpPut]
    public IActionResult Update(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var isUpdated = _emplloyeeRepository.Update(employeeConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _emplloyeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }

}
