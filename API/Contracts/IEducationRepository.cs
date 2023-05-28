﻿using API.Models;

namespace API.Contracts
{
    public interface IEducationRepository : IBaseRepository<Education>
    {
       IEnumerable<Education> GetByUniversityId(Guid guid);
        Education GetByEmployeeId(Guid employeeId);
    }
}
