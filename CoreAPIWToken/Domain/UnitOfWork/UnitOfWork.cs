using CoreAPIWToken.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TokenApiDBContext context;

        public UnitOfWork(TokenApiDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> CompleteAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
