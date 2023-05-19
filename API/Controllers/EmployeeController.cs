using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _emplloyeeRepository;
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _emplloyeeRepository = employeeRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _emplloyeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound();
        }

        return Ok(employees);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var room = _emplloyeeRepository.GetByGuid(guid);
        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        var result = _emplloyeeRepository.Create(employee);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(Employee employee)
    {
        var isUpdated = _emplloyeeRepository.Update(employee);
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
