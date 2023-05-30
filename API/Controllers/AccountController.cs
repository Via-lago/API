using API.Contracts;
using API.Models;
using API.Repositories;
using API.Utility;
using API.ViewModels;
using API.ViewModels.AccountRole;
using API.ViewModels.Accounts;
using API.ViewModels.Login;
using API.ViewModels.Response;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace API.Controllers;
[ApiController] 
[Route("api/[controller]")]
public class AccountController : BaseController<Account, AccountVM>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper<Account, AccountVM> _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
 
    
    public AccountController(IAccountRepository accountRepository,
                            IMapper<Account, AccountVM> mapper,
                            IEmployeeRepository employeeRepository,
                            IEmailService emailService,
                            ITokenService tokenService) : base (accountRepository, mapper)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _emailService = emailService;
        _tokenService = tokenService;

    }

    /*[HttpGet]
    public IActionResult GetAll()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return NotFound(new ResponseVM<List<AccountVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = accounts.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<AccountVM>>
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
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return NotFound(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var data = _mapper.Map(account);
        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success",
            Data = data
        });
    }*/

    [HttpPost("Register")]
    [AllowAnonymous]

    public IActionResult Register(RegisterVM registerVM)
    {

        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Registration failed"
                });
            case 1:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email already exists"
                });
            case 2:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Phone number already exists"
                });
            case 3:
                return Ok(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Registration success", 
                });
        }

        return Ok();

    }

    [HttpPost("Login")]
    [AllowAnonymous]

    public IActionResult Login(LoginVM loginVM)
    {
        var account = _accountRepository.Login(loginVM);
        var employee = _employeeRepository.GetByEmail(loginVM.Email);
        if (account == null)
        {
            return NotFound(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account Tidak Ditemukan"
            });
        }

        if (!Hashing.ValidatePassword(loginVM.Password,account.Password))
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password is invalid"
            });

        }
        var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,employee.nik),
                new(ClaimTypes.Name,$"{employee.FirstName}{employee.LastName}"),
                new(ClaimTypes.Email,employee.Email)
            };
        var roles = _accountRepository.GetRoles(employee.Guid);
        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role));
        }

        var token = _tokenService.GenerateToken(claims);

        return Ok(new ResponseVM<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login Success",
            Data = token
        });

    }


    /*[HttpPost]
    public IActionResult Create(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);

        var result = _accountRepository.Create(accountConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Create failed"
            });
        }
        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Create success",
        });
    }

    [HttpPut]
    public IActionResult Update(AccountVM accountVM)
    {
        var accountConverted = _mapper.Map(accountVM);
        var isUpdated = _accountRepository.Update(accountConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        var data = _mapper.Map(accountConverted);
        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success",
            Data = data
        });
    }*/

    [HttpPost("ChangePassword")]
    [AllowAnonymous]
    public IActionResult ChangePassword(ChangePasswordVM changePasswordVM)
    {
        // Cek apakah email dan OTP valid
        var account = _employeeRepository.FindGuidByEmail(changePasswordVM.Email);
        var changePass = _accountRepository.ChangePasswordAccount(account, changePasswordVM);
        switch (changePass)
        {
            case 0:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Registration failed"
                });
            case 1:
                return Ok(new ResponseVM<AccountRoleVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Password has been changed successfully"  
                });
            case 2:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Invalid OTP"
                });
            case 3:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP has already been used"
                });
            case 4:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "OTP expired"
                });
            case 5:
                return BadRequest(new ResponseVM<ChangePasswordVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Wrong Password No Same"
                });
            default:
                return BadRequest();
        }

    }
    [HttpPost("ForgotPassword/{email}")]
    [AllowAnonymous]
    public IActionResult UpdateResetPass(String email)
    {
        var getGuid = _employeeRepository.FindGuidByEmail(email);
        if (getGuid == null)
        {
            
            return NotFound(new ResponseVM<AccountEmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Akun tidak ditemukan"
            });
        }

        var isUpdated = _accountRepository.UpdateOTP(getGuid);

        switch (isUpdated)
        {
            case 0:
                return BadRequest(new ResponseVM<AccountEmployeeVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email tidak Ditemukan"
                });
            default:
                _emailService.SetEmail(email)
                 .SetSubject("Forgot Passowrd")
                 .SetHtmlMessage($"Your OTP is {isUpdated}")
                 .SendEmailAsync();


                return Ok(new ResponseVM<AccountEmployeeVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "OTP Succesfully Sent To Your Email",
                    Data = null
                });
        }
    }

    [HttpGet("GetByToken/{token}")]
    public IActionResult GetByToken(string token)
    {
        var data = _tokenService.ExtractClaimsFromJwt(token);
        if(data == null)
        {
            return NotFound(new ResponseVM<ClaimVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Invalid Token"
            });
        }
        return Ok(new ResponseVM<ClaimVM>
        {
            Code= StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Get Claims Success",
            Data = data
        });
    }





    /*[HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _accountRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<AccountVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Update failed"
            });
        }

        return Ok(new ResponseVM<AccountVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success"
        });
    }*/
}
