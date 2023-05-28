using Microsoft.AspNetCore.Mvc;
using API.Contracts;
using API.Models;
using API.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Universities;
using API.Utility;
using System.Data;
using API.ViewModels.Rooms;
using API.ViewModels.Response;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : BaseController<Role,RolesVM>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper<Role, RolesVM> _mapper;
    public RoleController(IRoleRepository roleRepository, 
                           IMapper<Role, RolesVM> mapper) : base(roleRepository, mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

   /* [HttpGet]
    public IActionResult GetAll()
    {
        var roles = _roleRepository.GetAll();
        if (!roles.Any())
        {
            return NotFound(new ResponseVM<List<RolesVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = roles.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<RolesVM>>
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
        var role = _roleRepository.GetByGuid(guid);
        if (role is null)
        {
            return NotFound(new ResponseVM<RolesVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = _mapper.Map(role);
        return Ok(new ResponseVM<RolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(RolesVM roleVM)
    {
        var RoleConverted = _mapper.Map(roleVM);
        var result = _roleRepository.Create(RoleConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<RolesVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed"
            });
        }

        var resultConverted = _mapper.Map(result);
        return Ok(new ResponseVM<RolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = resultConverted
        });
    }

    [HttpPut]
    public IActionResult Update(RolesVM roleVM)
    {
        var RoleConverted = _mapper.Map(roleVM);
        var isUpdated = _roleRepository.Update(RoleConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<RolesVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        var data = _mapper.Map(RoleConverted);
        return Ok(new ResponseVM<RolesVM>
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
        var isDeleted = _roleRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<RolesVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }
        return Ok(new ResponseVM<RolesVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success"
        });
    }*/

}
