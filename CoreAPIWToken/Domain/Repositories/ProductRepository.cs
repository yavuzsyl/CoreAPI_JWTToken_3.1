using CoreAPIWToken.Domain.Data;
using CoreAPIWToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Domain.Repositories
{
    public class ProductRepository : BaseRepository<Product> , IProductRepository
    {
        public ProductRepository(TokenApiDBContext context) : base(context)
        {

        }
    }
}
