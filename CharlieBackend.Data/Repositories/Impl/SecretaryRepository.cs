﻿using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CharlieBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using CharlieBackend.Data.Repositories.Impl.Interfaces;


namespace CharlieBackend.Data.Repositories.Impl
{
    public class SecretaryRepository : Repository<Secretary>, ISecretaryRepository
    {
        public SecretaryRepository(ApplicationContext applicationContext)
                : base(applicationContext)
        {
        }

        public new async Task<List<Secretary>> GetAllAsync()
        {
            return await _applicationContext.Secretaries
                .Include(secretary => secretary.Account).ThenInclude(x => x.Avatar)
                .ToListAsync();
        }
        public async Task<Secretary> GetSecretaryByAccountIdAsync(long accountId)
        {
            return await _applicationContext.Secretaries
                    .FirstOrDefaultAsync(secretary
                            => secretary.AccountId == accountId);
        }

        public new async Task<Secretary> GetByIdAsync(long id)
        {
            return await _applicationContext.Secretaries
                .Include(secretary => secretary.Account)
                .FirstOrDefaultAsync(secretary => secretary.Id == id);
        }

        public async Task<List<Secretary>> GetActiveAsync()
        {
            return await _applicationContext.Secretaries
                .Include(secretary => secretary.Account).ThenInclude(x => x.Avatar)
                .Where(sec => sec.Account.IsActive == true)
                .Select(sec => sec)
                .ToListAsync();
        }
    }
}
