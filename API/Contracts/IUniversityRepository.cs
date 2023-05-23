﻿using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IGenericRepository<University>
{
    IEnumerable<University> GetByName(string name);
}