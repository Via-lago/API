using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.AccountRole;
using API.ViewModels.Response;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : BaseController<AccountRole,AccountRoleVM>
{
    private readonly IAccountRoleRepository _accountroleRepository;
    private readonly IMapper<AccountRole, AccountRoleVM> _mapper;

    public AccountRoleController(IAccountRoleRepository accountroleRepository,
                                IMapper<AccountRole, AccountRoleVM> mapper) : base(accountroleRepository,mapper)
    {
        _accountroleRepository = accountroleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accountrole = _accountroleRepository.GetAll();
        if (!accountrole.Any())
        {
            return NotFound(new ResponseVM<List<AccountRoleVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = accountrole.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<AccountRoleVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountrole = _accountroleRepository.GetByGuid(guid);
        if (accountrole is null)
        {
            return NotFound(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = _mapper.Map(accountrole);
        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(AccountRoleVM accountroleVM)
    {
        var AccountRoleConverted = _mapper.Map(accountroleVM);
        var result = _accountroleRepository.Create(AccountRoleConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed"
            });
        }
        var resultConverted = _mapper.Map(result);
        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = resultConverted
        });
    }

    [HttpPut]
    public IActionResult Update(AccountRoleVM accountroleVM)
    {
        var AccountRoleConverted = _mapper.Map(accountroleVM);
        var isUpdated = _accountroleRepository.Update(AccountRoleConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        var data = _mapper.Map(AccountRoleConverted);
        return Ok(new ResponseVM<AccountRoleVM>
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
        var isDeleted = _accountroleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountRoleVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        return Ok(new ResponseVM<AccountRoleVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success"
        });
    }
}

