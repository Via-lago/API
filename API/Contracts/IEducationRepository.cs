﻿using API.Models;

namespace API.Contracts
{
    public interface IEducationRepository : IGenericRepository<Education>
    {
       IEnumerable<Education> GetByUniversityId(Guid guid);
        Education GetByEmployeeId(Guid employeeId);
    }
}
