using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using API.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Universities;
using API.Utility;
using System.Data;
using API.ViewModels.Rooms;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RolesVM> _mapper;
    public RoleController(IRoleRepository roleRepository, 
                           IMapper<Role, RolesVM> mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var roles = _roleRepository.GetAll();
        if (!roles.Any())
        {
            return NotFound();
        }
        var data = roles.Select(_mapper.Map).ToList();
        return Ok(data);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound();
        }
        var data = _mapper.Map(role);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(RolesVM roleVM)
    {
        var RoleConverted = _mapper.Map(roleVM);
        var result = _roleRepository.Create(RoleConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(RolesVM roleVM)
    {
        var RoleConverted = _mapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(RoleConverted);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }
        /*var data = _mapper.Map(isDeleted);*/
        return Ok();
    }

}
