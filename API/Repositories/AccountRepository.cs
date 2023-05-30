using API.Contexts;
using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.Accounts;
using API.ViewModels.Employees;
using API.ViewModels.Login;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingManagementDbContext context, 
            IUniversityRepository universityRepository,
            IEmployeeRepository employeeRepository,
            IEducationRepository educationRepository,
            IAccountRoleRepository accountRoleRepository,
            IRoleRepository roleRepository,
            ITokenService tokenService): base(context) 
        {
            _universityRepository = universityRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
            _tokenService = tokenService;


        }

        private readonly IUniversityRepository _universityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly ITokenService _tokenService;

        public Account GetByEmployeeId(Guid? employeeId)
        {
            return _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
        }

        public LoginVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var employee = _employeeRepository.GetAll();
            var query = from emp in employee
                        join acc in account
                        on emp.Guid equals acc.Guid
                        where emp.Email == loginVM.Email
                        select new LoginVM
                        {
                            Email = emp.Email,
                            Password = acc.Password

                        };
            return query.FirstOrDefault();
        }
        public int UpdateOTP(Guid? employeeId)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            //Generate OTP
            Random rnd = new Random();
            var getOtp = rnd.Next(100000, 999999);
            account.Otp = getOtp;

            //Add 5 minutes to expired time
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            try
            {
                var check = Update(account);


                if (!check)
                {
                    return 0;
                }
                return getOtp;
            }
            catch
            {
                return 0;
            }
        }
        public int Register(RegisterVM registerVM)
        {
            try
            {
                var university = new University
                {
                    Code = registerVM.UniversityCode,
                    Name = registerVM.UniversityName

                };
                _universityRepository.CreateWithValidate(university);

                var employee = new Employee
                {
                    nik = GenerateNIK(),
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    HiringDate = registerVM.HiringDate,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                 _employeeRepository.Create(employee);

                var education = new Education
                {
                    Guid = employee.Guid,
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    Gpa = registerVM.GPA,
                    UniversityGuid = university.Guid
                };
                _educationRepository.Create(education);

                
                var account = new Account
                {
                    Guid = employee.Guid,
                    Password = Hashing.HashPassword(registerVM.Password),
                    IsDeleted = false,
                    IsUsed = true,
                    Otp = 0
                };

                Create(account);
                var accountRole = new AccountRole
                {
                    RoleGuid = Guid.Parse("02bf5069-f27e-40ba-6f98-08db60bf3fc7"),
                    AccountGuid = employee.Guid
                };

                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();

                return 3;

            }
            catch
            {
                return 0;
            }

        }
        private string GenerateNIK()
        {
            var lastNik = _employeeRepository.GetAll().OrderByDescending(e => int.Parse(e.nik)).FirstOrDefault();

            if (lastNik != null)
            {
                int lastNikNumber;
                if (int.TryParse(lastNik.nik, out lastNikNumber))
                {
                    return (lastNikNumber + 1).ToString();
                }
            }

            return "100000";
        }
        public int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            if (account == null || account.Otp != changePasswordVM.OTP)
            {
                return 2;
            }
            // Cek apakah OTP sudah digunakan
            if (account.IsUsed)
            {
                return 3;
            }
            // Cek apakah OTP sudah expired
            if (account.ExpiredTime < DateTime.Now)
            {
                return 4;
            }
            // Cek apakah NewPassword dan ConfirmPassword sesuai
            if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
            {
                return 5;
            }
            // Update password
            account.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
            account.IsUsed = true;
            try
            {
                var updatePassword = Update(account);
                if (!updatePassword)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<string> GetRoles(Guid guid)
        {

            var accountrole = _accountRoleRepository.GetAll();
            var roles = _roleRepository.GetAll();

            var getAccount = GetByGuid(guid);
            if(getAccount == null) return Enumerable.Empty<string>();
            var GetRoles = from a in accountrole
                           join r in roles on
                           a.RoleGuid equals r.Guid
                           where a.AccountGuid == guid
                           select r.Name;
            
            return GetRoles.ToList();

        }
    }
}

