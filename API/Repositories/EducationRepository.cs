﻿using API.Contexts;
using API.Contracts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EducationRepository : GenericRepository<Education>, IEducationRepository
    {
        public EducationRepository(BookingManagementDbContext context) : base(context) { }
        public IEnumerable<Education> GetByUniversityId(Guid universityId)
        {
            return _context.Set<Education>().Where(e => e.UniversityGuid == universityId);
        }
    }
}
