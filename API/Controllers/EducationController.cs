using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.Educations;
using API.ViewModels.Response;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : BaseController<Education,EducationsVM>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationsVM> _educationMapper;
    public EducationController(IEducationRepository educationRepository, 
                                IMapper<Education, EducationsVM> educationMapper) : base (educationRepository,educationMapper)
    {
        _educationRepository = educationRepository;
        _educationMapper = educationMapper;
    }

  /*  [HttpGet]
    public IActionResult GetAll()
    {
        var educations = _educationRepository.GetAll();
        if (!educations.Any())
        {
            return NotFound(new ResponseVM<List<EducationsVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = educations.Select(_educationMapper.Map).ToList();
        return Ok(new ResponseVM<List<EducationsVM>>
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
        var education = _educationRepository.GetByGuid(guid);
        if (education is null)
        {
            return NotFound(new ResponseVM<EducationsVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = _educationMapper.Map(education);
        return Ok(new ResponseVM<EducationsVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(EducationsVM educationVM)
    {
        var educationConverted = _educationMapper.Map(educationVM);
        var result = _educationRepository.Create(educationConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EducationsVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed"
            });
        }

        var resultConverted = _educationMapper.Map(result);
        return Ok(new ResponseVM<EducationsVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
            Data = resultConverted
        });
    }

    [HttpPut]
    public IActionResult Update(EducationsVM educationVM)
    {
        var educationConverted = _educationMapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EducationsVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        var data = _educationMapper.Map(educationConverted);
        return Ok(new ResponseVM<EducationsVM>
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
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EducationsVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        return Ok(new ResponseVM<EducationsVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success"
        });
    }*/
}
