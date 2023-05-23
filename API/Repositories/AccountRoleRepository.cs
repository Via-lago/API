﻿using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : GenericRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context) { }
    }
}
