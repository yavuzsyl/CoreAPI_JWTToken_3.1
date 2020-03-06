using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
    }
}
