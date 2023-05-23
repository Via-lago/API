using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels.AccountRole;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountRoleController : ControllerBase
{
    private readonly IAccountRoleRepository _accountroleRepository;
    private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
    public AccountRoleController(IAccountRoleRepository accountroleRepository,
                                IMapper<AccountRole, AccountRoleVM> mapper)
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
            return NotFound();
        }
        var data = accountrole.Select(_mapper.Map).ToList();
        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var accountrole = _accountroleRepository.GetByGuid(guid);
        if (accountrole is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(accountrole);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(AccountRoleVM accountroleVM)
    {
        var AccountRoleConverted = _mapper.Map(accountroleVM);
        var result = _accountroleRepository.Create(AccountRoleConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(AccountRoleVM accountroleVM)
    {
        var AccountRoleConverted = _mapper.Map(accountroleVM);
        var isUpdated = _accountroleRepository.Update(AccountRoleConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountroleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}

