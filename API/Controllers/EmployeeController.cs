


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
using API.Repositories;
using API.ViewModels.Response;
using API.ViewModels.Rooms;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeController : BaseController<Employee,EmployeeVM>
{
    private readonly IEmployeeRepository _emplloyeeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IMapper<Employee, EmployeeVM> _mapper;
    public EmployeeController(IEmployeeRepository employeeRepository,
                                IMapper<Employee, EmployeeVM> mapper,
                                IAccountRepository accountRepository,
                                IEducationRepository educationRepository,
                                IUniversityRepository universityRepository)
                                : base (employeeRepository,mapper)
    {
        _emplloyeeRepository = employeeRepository;
        _mapper = mapper;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
        _accountRepository = accountRepository;
    }

    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAll()
    {
        var masterEmployees = _emplloyeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound(new ResponseVM<List<MasterEmployeeVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        return Ok(new ResponseVM<List<MasterEmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success"
        });
   }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid(Guid guid)
    {
        var masterEmployees = _emplloyeeRepository.GetMasterEmployeeByGuid(guid);
        if (masterEmployees is null)
        {
            return NotFound(new ResponseVM<MasterEmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }

        return Ok(new ResponseVM<MasterEmployeeVM>
        {
            Code = 200,
            Status = "OK",
            Message = "Employee Found",
            Data = masterEmployees
        });
    }

    /*[HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var employee = _emplloyeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = _mapper.Map(employee);
        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var result = _emplloyeeRepository.Create(employeeConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed"
            });
        }
        var resultConverted = _mapper.Map(result);
        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = resultConverted
        });
    }
    [HttpPut]
    public IActionResult Update(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var isUpdated = _emplloyeeRepository.Update(employeeConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        var data = _mapper.Map(employeeConverted);
        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success",
            Data = data
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _emplloyeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        return Ok(new ResponseVM<UniversityVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success"
        });
    }*/

}
