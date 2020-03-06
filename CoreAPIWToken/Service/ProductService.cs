using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Domain.Repositories;
using CoreAPIWToken.Domain.Response;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {

        public ProductService(IUnitOfWork unitOfWork, IProductRepository repository) : base(unitOfWork, repository)
        {

        }


    }
}
